using NZWalks.Models;

namespace NZWalks.Repository.Interface
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);    
    }
}
