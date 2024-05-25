using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PaymentService
    {
        public readonly UsersService _usersService;
        public PaymentService(UsersService _usersService)
        {
            this._usersService = _usersService;
        }
        public double CalculatePayment(Guid userId)
        {
            UserDto user = _usersService.GetUser(userId);

            IPaymentStrategy paymentStrategy;

            if (user.ImageProfile != null && user.ImageProfile != "string")
            {
                paymentStrategy = new DiscountProfileImageStrategy();
            }
            else
            {
                paymentStrategy = new NoDiscountStrategy();
            }

            PaymentCalculator paymentCalculator = new PaymentCalculator(paymentStrategy, _usersService);
            return paymentCalculator.GetUserHasToPay(userId);
        }

    }

}
