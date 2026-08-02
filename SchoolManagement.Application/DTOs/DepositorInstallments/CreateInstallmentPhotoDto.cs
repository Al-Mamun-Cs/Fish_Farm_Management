using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.DTOs.DepositorInstallments
{
    public class CreateInstallmentPhotoDto
    {
        

        public IFormFile Photo { get; set; }
        public CreateDepositorInstallmentDto DepositorInstallmentForm { get; set; }
}
}
