using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels
{
    public class ResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public string? Token { get; set; }

        public string? TokenConfirm { get; set; }
    }
}
