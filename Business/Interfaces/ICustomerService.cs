using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Skapar en ny kund.
        /// Returnerar true om skapandet lyckas, annars false.
        /// </summary>
        Task<bool> CreateCustomerAsync(CustomerCreateDto dto);

        /// <summary>
        /// Hämtar en lista över alla kunder med detaljer.
        /// </summary>
        Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();

        /// <summary>
        /// Hämtar en kund med detaljer baserat på ID.
        /// Returnerar kundmodellen om kunden hittas, annars null.
        /// </summary>
        Task<CustomerModel?> GetCustomerWithDetailsAsync(int id);

        /// <summary>
        /// Uppdaterar en existerande kund.
        /// Returnerar true om uppdateringen lyckas, annars false.
        /// </summary>
        Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDto dto);

        /// <summary>
        /// Tar bort en kund baserat på ID.
        /// Returnerar true om borttagningen lyckas, annars false.
        /// </summary>
        Task<bool> DeleteCustomerAsync(int id);
    }
}
