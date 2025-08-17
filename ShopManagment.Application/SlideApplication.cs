using _0_FrameWork.Application;
using ShopManagment.Application.Contracts.Slide;
using ShopManagment.Domain.ProductPictureAgg;
using ShopManagment.Domain.SliderAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManagment.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IFileUploader _fileUploader;

        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operationResult = new OperationResult();

            var path = "Slides";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            var slide = new Slide(picturePath,command.PictureAlt, command.PictureTitle, command.Heading, command.Title, command.Text, command.BtnText);
            _slideRepository.Create(slide);
            _slideRepository.SaveChange();
            return operationResult.Succedded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(command.Id);
            if (slide == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            string fileName = null;

            if (command.Picture != null && command.Picture.Length > 0)
            {
                var path = "Slides";
                fileName = _fileUploader.Upload(command.Picture, path);
            }
            else
            {
                fileName = command.ExistingPicturePath;
            }

            slide.Edit(fileName, command.PictureAlt, command.Title, command.Heading, command.Title, command.Text, command.BtnText);
            _slideRepository.SaveChange();
            return operationResult.Succedded();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            slide.Remove();
            _slideRepository.SaveChange();
            return operationResult.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            slide.Restore();
            _slideRepository.SaveChange();
            return operationResult.Succedded();
        }
    }
}
