using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Dto.RequestDto;
using NZWalks.Models;
using NZWalks.Repository.Interface;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _repository;

        public ImageController(IMapper mapper, IImageRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageRequestDto requestDto)
        {
            ValidateFileUpload(requestDto);


            if (ModelState.IsValid)
            {
                var imageDomain = _mapper.Map<Image>(requestDto);

                await _repository.Upload(imageDomain);

                return Ok(imageDomain);
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageRequestDto requestDto)
        {
            var allowedExtension = new string[]
            {
                ".jpg",
                ".jpeg",
                ".png"
            };

            if (!allowedExtension.Contains(Path.GetExtension(requestDto.File.FileName)))
            {
                ModelState.AddModelError("File", "Dosen't Support The Given Extension");
            }

            if (requestDto.File.Length > 10495760)
            {
                ModelState.AddModelError("File", "Provided File Size Is Too Big Need A File Size Upto 10MB");
            }
        }
    }
}
