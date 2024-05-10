using DonationAPI1.Data;
using DonationAPI1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonationAPI1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DonationsController : Controller
    {
        private readonly DonationAPIDbContext dbContext;

        public DonationsController(DonationAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetDoantions()
        {
            return Ok(await dbContext.Donations.ToListAsync());
            
        }

        [HttpGet]
        [Route("{Id:guid}")]
        public async Task<IActionResult> GetDonation([FromRoute] Guid Id)
        {
            var donation = await dbContext.Donations.FindAsync(Id);

            if (donation == null) 
            {
                return NotFound();
            }

            return Ok(donation);
        }

        [HttpPost]
        public async Task<IActionResult> AddDonation(AddDonationRequest addDonationRequest)
        {
            var donation = new Donation()
            {
                Id = Guid.NewGuid(),
                Address = addDonationRequest.Address,
                Email = addDonationRequest.Email,
                FullName = addDonationRequest.FullName,
                Phone = addDonationRequest.Phone,
            };

            await dbContext.Donations.AddAsync(donation);
            await dbContext.SaveChangesAsync();

            return Ok(donation);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateDonation([FromRoute] Guid Id, UpdateDonationRequest updateDonationRequest)
        {
            var donation = await dbContext.Donations.FindAsync(Id);

            if (donation != null) 
            {
                donation.FullName = updateDonationRequest.FullName;
                donation.Address = updateDonationRequest.Address;
                donation.Phone = updateDonationRequest.Phone;
                donation.Email = updateDonationRequest.Email;

                await dbContext.SaveChangesAsync();

                return Ok(donation);    
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> DeleteDonation([FromRoute] Guid Id)
        {
            var donation = await dbContext.Donations.FindAsync(Id);

            if (donation != null)
            {
                dbContext.Remove(donation);
                await dbContext.SaveChangesAsync();
                return Ok(donation); // OR return Ok(Donation Deleted):
            }

            return NotFound();
        }
    }
}
