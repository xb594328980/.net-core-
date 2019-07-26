using System;
using System.ComponentModel.DataAnnotations;

namespace MicroService.IdentityServer.Host.Extensions
{
    public class User
    {
        public User()
        {

        }
        public User(int id,
            string openId,
            string nickName,
            string nickPic,
            string nickPhone,
            int gender,
            string city,
            string province,
            string country,
            DateTime createTime, int state, DateTime? updateTime)
        {
            Id = id;
            OpenId = openId;
            NickName = nickName;
            NickPic = nickPic;
            NickPhone = nickPhone;
            Gender = gender;
            City = city;
            Province = province;
            Country = country;
            CreateTime = createTime;
            this.State = state;
            this.UpdateTime = updateTime;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户openId
        /// </summary>
        public string OpenId { get;  set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get;  set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string NickPic { get;  set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string NickPhone { get;  set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get;  set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get;  set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get;  set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get;  set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get;  set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get;  set; }


        /// <summary>
        /// 状态
        /// </summary>
        public int State { get;  set; }
    }
}