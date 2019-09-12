using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public interface IApiKeyService
    {
        Task<bool> IsApiKeyValid(string siteBaseUrl, string key);
    }
}
