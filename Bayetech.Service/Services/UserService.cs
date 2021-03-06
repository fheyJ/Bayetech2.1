﻿using Newtonsoft.Json.Linq;
using Bayetech.DAL;
using Newtonsoft.Json;
using Bayetech.Core.Entity;
using System;
using Bayetech.Core;
using System.Web;
using System.Linq;
using System.Text;

namespace Bayetech.Service
{
    public class UserService :BaseService<User>, IUserService
    {
        public bool CheckAccount(string account)
        {
            throw new NotImplementedException();
        }

        public bool CreatAccount(JObject json)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登陆校验
        /// </summary>
        /// <param name="username">登录账号</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>

        public JObject CheckLogin(JObject json)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                User _account = (User)JsonConvert.DeserializeObject(json.First.Path, typeof(User));//转换对象
                User _userEntity = db.FindEntity<User>(t => t.Name == _account.Name);
                JObject result = new JObject();
                if (_userEntity != null)
                {
                    if ((bool)_userEntity.IsValiteLogin)
                    {
                        //UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
                        //string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                        var userName = _account.Name;
                        string dbPassword = Md5.EncryptString(_account.Password);
                        if (dbPassword == _userEntity.Password)
                        {
                            Login _currentLogin = new Login();
                            _currentLogin.UserName = _account.Name;
                            _currentLogin.PassWord = dbPassword;
                            _currentLogin.LoginIp = Common.GetHostAddress();
                            _currentLogin.LoginTime = DateTime.Now;
                            _currentLogin.Message = "登录成功";
                            db.Insert(_currentLogin);
                            db.Commit();
                            result.Add(ResultInfo.Result, JToken.FromObject(true));
                            result.Add(ResultInfo.Content, JToken.FromObject(_currentLogin.Message));

                            //创建标识
                            var token = GetToken(userName);
                            //比对缓存没有则重新生成
                            Token cookie = (Token)HttpRuntime.Cache.Get(userName);
                            if (HttpRuntime.Cache.Get(userName) == null)
                            {
                                cookie = new Token();
                                cookie.StaffId = userName;
                                cookie.TokenId = token;
                                cookie.ExpireTime = DateTime.Now.AddHours(12);//设置12小时过期
                                HttpRuntime.Cache.Insert(cookie.StaffId.ToString(), cookie, null, cookie.ExpireTime, TimeSpan.Zero);
                            }
                             HttpContext.Current.Session["token-" + _account.Name] = token;
                            //HttpContext.Current.Response.Cookies["token"].Value = token;
                            //HttpContext.Current.Response.Cookies["token"].Expires.AddDays(1);
                        }
                        else
                        {
                            result.Add(ResultInfo.Result, JToken.FromObject(false));
                            result.Add(ResultInfo.Content, JToken.FromObject("密码错误,请重新尝试!"));
                        }
                    }
                    else
                    {
                        result.Add(ResultInfo.Result, JToken.FromObject(false));
                        result.Add(ResultInfo.Content, JToken.FromObject("账户被系统锁定,请联系管理员!"));
                    }
                }
                else
                {
                    result.Add(ResultInfo.Result, JToken.FromObject(false));
                    result.Add(ResultInfo.Content, JToken.FromObject("账户不存在，请重新输入!"));
                }
                return result;
            }
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetToken(string str)
        {
            string timeStamp = GetTimeStamp();
            string nonce = GetRandom();
            var hash = System.Security.Cryptography.MD5.Create();
            //拼接签名数据
            var signStr = str+ timeStamp + nonce + Guid.NewGuid().ToString();
            //将字符串中字符按升序排序
            var sortStr = string.Concat(signStr.Reverse().OrderByDescending(c => c));
            var bytes = Encoding.UTF8.GetBytes(sortStr);
            //使用MD5加密
            var md5Val = hash.ComputeHash(bytes);
            //把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            foreach (var c in md5Val)
            {
                result.Append(c.ToString("X2"));
            }
            return result.ToString().ToUpper();
        }


        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>  
        /// 获取随机数
        /// </summary>  
        /// <returns></returns>  
        private static string GetRandom()
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int i = rd.Next(0, int.MaxValue);
            return i.ToString();
        }
    }
}
