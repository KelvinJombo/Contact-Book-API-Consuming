using MyContactBookAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContactBookAPI.Core.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
