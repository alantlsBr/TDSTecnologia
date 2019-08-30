using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TDSTecnologia.Site.Core.Utilitarios
{
    public class UtilImagem
    {
        public byte[] ConvertarParaByte(IFormFile arquivo)
        {
            
            if (arquivo != null && arquivo.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                arquivo.OpenReadStream().CopyToAsync(ms);
                return ms.ToArray();
            }

            return null;

        }
    }
}
