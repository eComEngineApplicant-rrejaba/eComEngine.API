using eComEngine.API.Entitity;
using System.ComponentModel.DataAnnotations;

namespace eComEngine.API.DTO
{
    public class NameDTO
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }

    public class AddressDTO
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
    }

    internal static class PhoneType
    {
        public static string Home = "Home";
        public static string Work = "Work";
        public static string Mobile = "Mobile";
    }

    public class PhoneDTO
    {
        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class ContactDTO
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = $"Not a valid email")]
        public string Email { get; set; }

        public NameDTO Name { get; set; }
        public AddressDTO Address { get; set; }
        public List<PhoneDTO> Phone { get; set; }

        /// <summary>
        /// Map DTO to Entity
        /// </summary>
        /// <param name="contactDTO"></param>
        /// <returns></returns>
        public ContactEntity ToEntity(ContactDTO contactDTO)
        {
            ContactEntity entity = new ContactEntity();

            entity.Id = contactDTO.Id;
            entity.Email = contactDTO.Email;
            entity.Name = new NameEntity()
            {
                First = contactDTO.Name.First,
                Middle = contactDTO.Name.Middle,
                Last = contactDTO.Name.Last
            };
            entity.Address = new AddressEntity()
            {
                City = contactDTO.Address.City,
                State = contactDTO.Address.State,
                Street = contactDTO.Address.Street,
                Zip = contactDTO.Address.Zip
            };
            entity.Phone = new List<PhoneEntity>();

            foreach (var phone in contactDTO.Phone)
            {
                entity.Phone.Add(new PhoneEntity()
                {
                    Number = phone.Number,
                    Type = phone.Type
                });
            }

            return entity;
        }

        /// <summary>
        /// Map Entity to DTO
        /// </summary>
        /// <param name="contactEntity"></param>
        public void ToDTO(ContactEntity contactEntity)
        {
            Id = contactEntity.Id;
            Email = contactEntity.Email;
            Name = new NameDTO()
            {
                First = contactEntity.Name.First,
                Middle = contactEntity.Name.Middle,
                Last = contactEntity.Name.Last
            };
            Address = new AddressDTO()
            {
                City = contactEntity.Address.City,
                State = contactEntity.Address.State,
                Street = contactEntity.Address.Street,
                Zip = contactEntity.Address.Zip
            };
            Phone = new List<PhoneDTO>();

            foreach (var phone in contactEntity.Phone)
            {
                Phone.Add(new PhoneDTO()
                {
                    Number = phone.Number,
                    Type = phone.Type
                });
            }
        }

        /// <summary>
        /// Validate Data
        /// </summary>
        /// <returns></returns>
        public List<string> ValidateDTO()
        {
            List<string> validationError = new List<string>();

            foreach (var phone in Phone)
            {
                if(string.Compare(phone.Type,PhoneType.Home,true) != 0 &&
                    string.Compare(phone.Type, PhoneType.Mobile, true) != 0 &&
                    string.Compare(phone.Type, PhoneType.Work, true) != 0)
                {
                    validationError.Add($"Invalid Phone Type of {phone.Type}");
                }
            }

            return validationError;
        }
    }
}
