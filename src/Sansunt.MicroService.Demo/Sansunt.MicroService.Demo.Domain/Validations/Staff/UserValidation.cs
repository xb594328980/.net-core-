using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Sansunt.MicroService.Demo.Domain.Commands.Staff;

namespace Sansunt.MicroService.Demo.Domain.Validations.Staff
{
    public class UserValidation<T> : BaseValidation<T> where T : StaffCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);
        }
        protected void ValidateOpenId()
        {
            RuleFor(c => c.OpenId)
                .NotEmpty();
        }
        protected void ValidateNickPhone()
        {
            RuleFor(c => c.NickPhone)
                .Must(HavePhone)
                .WithMessage("电话应该为11-13位");
        }
        protected void ValidateNickPic()
        {
            RuleFor(c => c.NickPic)
                .NotEmpty().WithMessage("头像不能为空");
        }
        protected void ValidateNickName()
        {
            RuleFor(c => c.NickName)
                .NotEmpty().WithMessage("昵称不能为空");
        }

        protected void ValidateGender()
        {
            RuleFor(c => c.Gender)
                .Must(x => (x == 0 || x == 1 || x == 2))
                .WithMessage("性别必须为0或1或2");
        }
        protected void ValidateCity()
        {
            RuleFor(c => c.City)
                .Length(0, 10).WithMessage("城市长度在0~10个字符之间");
        }

        protected void ValidateCountry()
        {
            RuleFor(c => c.Country)
                .Length(0, 10).WithMessage("国家长度在0~10个字符之间");
        }
        protected void ValidateProvince()
        {
            RuleFor(c => c.Province)
                .Length(0, 10).WithMessage("省份长度在0~10个字符之间");
        }
        protected void ValidateCreateTime()
        {
            RuleFor(c => c.CreateTime)
                .NotEmpty()
                .GreaterThan(new DateTime(1970, 1, 1)).WithMessage("创建时间不合法");
        }

        protected void ValidateUpdateTime()
        {
            RuleFor(c => c.UpdateTime)
                .NotEmpty()
                .GreaterThan(new DateTime(1970, 1, 1)).WithMessage("更新时间不合法");
        }





        protected bool HavePhone(string phone)
        {
            return string.IsNullOrEmpty(phone) || IsTelephone(phone) || IsMobile(phone);
        }
    }
}
