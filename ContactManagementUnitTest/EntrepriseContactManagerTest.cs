using AutoMapper;
using ContactManagementService.Entities;
using ContactManagementService.Models;
using ContactManagementService.Services;
using ContactManagementService.StorageAccess.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactManagementUnitTest
{
    public class EntrepriseContactManagerTest
    {
        [Fact]
        public async Task AddEntrepriseContact_EntrepriseNotFound_ThrowsException()
        {
            //Arrange
            Mock<IEntrepriseContactStorageManager> entrepriseContactStorageManager = new Mock<IEntrepriseContactStorageManager>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IEntrepriseStorageManager> entrepriseStorageManager = new Mock<IEntrepriseStorageManager>();
            Mock<IContactStorageManager> contactStorageManager = new Mock<IContactStorageManager>();

            EntrepriseContactManager contactManager = new EntrepriseContactManager(entrepriseContactStorageManager.Object, mapper.Object, entrepriseStorageManager.Object, contactStorageManager.Object);
            entrepriseStorageManager.Setup(x => x.GetEntreprise(It.IsAny<int>())).ReturnsAsync((Entreprise)null);

            //Act


            //Assert
            await Assert.ThrowsAsync<KeyNotFoundException>( () =>  contactManager.AddEntrepriseContact(new EntrepriseContactModel
            {
                ContactId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Employee,
                EntrepriseId = 1
            }));
        }

        [Fact]
        public async Task AddEntrepriseContact_ContactNotFound_ThrowsException()
        {
            //Arrange
            Mock<IEntrepriseContactStorageManager> entrepriseContactStorageManager = new Mock<IEntrepriseContactStorageManager>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IEntrepriseStorageManager> entrepriseStorageManager = new Mock<IEntrepriseStorageManager>();
            Mock<IContactStorageManager> contactStorageManager = new Mock<IContactStorageManager>();

            EntrepriseContactManager contactManager = new EntrepriseContactManager(entrepriseContactStorageManager.Object, mapper.Object, entrepriseStorageManager.Object, contactStorageManager.Object);
            entrepriseStorageManager.Setup(x => x.GetEntreprise(It.IsAny<int>())).ReturnsAsync(new Entreprise
            {
                Id = 1
            });

            contactStorageManager.Setup(x => x.GetContact(It.IsAny<int>())).ReturnsAsync((Contact)null);

            //Act


            //Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.AddEntrepriseContact(new EntrepriseContactModel
            {
                ContactId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Employee,
                EntrepriseId = 1
            }));
        }

        [Fact]
        public async Task AddEntrepriseContact_FreeLancerNoVat_ThrowsException()
        {
            //Arrange
            Mock<IEntrepriseContactStorageManager> entrepriseContactStorageManager = new Mock<IEntrepriseContactStorageManager>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IEntrepriseStorageManager> entrepriseStorageManager = new Mock<IEntrepriseStorageManager>();
            Mock<IContactStorageManager> contactStorageManager = new Mock<IContactStorageManager>();

            EntrepriseContactManager contactManager = new EntrepriseContactManager(entrepriseContactStorageManager.Object, mapper.Object, entrepriseStorageManager.Object, contactStorageManager.Object);
            entrepriseStorageManager.Setup(x => x.GetEntreprise(It.IsAny<int>())).ReturnsAsync(new Entreprise
            {
                Id = 1
            });

            contactStorageManager.Setup(x => x.GetContact(It.IsAny<int>())).ReturnsAsync(new Contact { 
                Id = 1
            });

            //Act


            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => contactManager.AddEntrepriseContact(new EntrepriseContactModel
            {
                ContactId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Freelancer,
                EntrepriseId = 1
            }));
        }

        [Fact]
        public async Task AddEntrepriseContact_AllOk_ReturnsModel()
        {
            //Arrange
            Mock<IEntrepriseContactStorageManager> entrepriseContactStorageManager = new Mock<IEntrepriseContactStorageManager>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IEntrepriseStorageManager> entrepriseStorageManager = new Mock<IEntrepriseStorageManager>();
            Mock<IContactStorageManager> contactStorageManager = new Mock<IContactStorageManager>();

            EntrepriseContactManager contactManager = new EntrepriseContactManager(entrepriseContactStorageManager.Object, mapper.Object, entrepriseStorageManager.Object, contactStorageManager.Object);
            entrepriseStorageManager.Setup(x => x.GetEntreprise(It.IsAny<int>())).ReturnsAsync(new Entreprise
            {
                Id = 1
            });

            contactStorageManager.Setup(x => x.GetContact(It.IsAny<int>())).ReturnsAsync(new Contact
            {
                Id = 1
            });

            EntrepriseContactModel contactModel = new EntrepriseContactModel
            {
                ContactId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Freelancer,
                EntrepriseId = 1,
                VATNumber = "123456"
            };

            EntrepriseContact entrepriseContact = new EntrepriseContact
            {
                ContactId = 1,
                VATNumber = "123456",
                EntrepriseId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Freelancer
            };

            mapper.Setup(x => x.Map<EntrepriseContact>(contactModel)).Returns(entrepriseContact);

            entrepriseContactStorageManager.Setup(x => x.AddEntrepriseContact(It.IsAny<EntrepriseContact>())).ReturnsAsync(contactModel);


            //Act
            EntrepriseContactModel entrepriseContactModel = await contactManager.AddEntrepriseContact(new EntrepriseContactModel
            {
                ContactId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Freelancer,
                EntrepriseId = 1,
                VATNumber = "123456"
            });

            //Assert
            Assert.Equal(contactModel.ContactId, entrepriseContactModel.ContactId);
            Assert.Equal(contactModel.ContactType, entrepriseContactModel.ContactType);
            Assert.Equal(contactModel.EntrepriseId, entrepriseContactModel.EntrepriseId);
            Assert.Equal(contactModel.VATNumber, entrepriseContactModel.VATNumber);
        }

        [Fact]
        public async Task DeleteEntrepriseContact_EntrepriseContactNotFound_ThrowsException()
        {
            //Arrange
            Mock<IEntrepriseContactStorageManager> entrepriseContactStorageManager = new Mock<IEntrepriseContactStorageManager>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IEntrepriseStorageManager> entrepriseStorageManager = new Mock<IEntrepriseStorageManager>();
            Mock<IContactStorageManager> contactStorageManager = new Mock<IContactStorageManager>();

            EntrepriseContactManager contactManager = new EntrepriseContactManager(entrepriseContactStorageManager.Object, mapper.Object, entrepriseStorageManager.Object, contactStorageManager.Object);
            entrepriseContactStorageManager.Setup(x => x.GetEntrepriseContact(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((EntrepriseContact)null);

            //Act


            //Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.DeleteEntrepriseContract(1,1));
        }

        [Fact]
        public async Task DeleteEntrepriseContact_AllOk_CallsWithNoError()
        {
            //Arrange
            Mock<IEntrepriseContactStorageManager> entrepriseContactStorageManager = new Mock<IEntrepriseContactStorageManager>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IEntrepriseStorageManager> entrepriseStorageManager = new Mock<IEntrepriseStorageManager>();
            Mock<IContactStorageManager> contactStorageManager = new Mock<IContactStorageManager>();

            EntrepriseContactManager contactManager = new EntrepriseContactManager(entrepriseContactStorageManager.Object, mapper.Object, entrepriseStorageManager.Object, contactStorageManager.Object);
            EntrepriseContact entrepriseContact = new EntrepriseContact
            {
                ContactId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Employee,
                EntrepriseId = 1
            };
            entrepriseContactStorageManager.Setup(x => x.GetEntrepriseContact(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(entrepriseContact);

            //Act
            await contactManager.DeleteEntrepriseContract(1, 1);

            //Assert
            entrepriseContactStorageManager.Verify(x => x.DeleteEntrepriseContact(entrepriseContact), Times.Once);
        }

        [Fact]
        public async Task UpdateEntrepriseContact_EntrepriseContactNotFound_ThrowsException()
        {
            //Arrange
            Mock<IEntrepriseContactStorageManager> entrepriseContactStorageManager = new Mock<IEntrepriseContactStorageManager>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IEntrepriseStorageManager> entrepriseStorageManager = new Mock<IEntrepriseStorageManager>();
            Mock<IContactStorageManager> contactStorageManager = new Mock<IContactStorageManager>();

            EntrepriseContactManager contactManager = new EntrepriseContactManager(entrepriseContactStorageManager.Object, mapper.Object, entrepriseStorageManager.Object, contactStorageManager.Object);
            entrepriseContactStorageManager.Setup(x => x.GetEntrepriseContact(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((EntrepriseContact)null);

            //Act


            //Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.UpdateEntrepriseContact(new EntrepriseContactModel
            {
                ContactId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Employee,
                EntrepriseId =1
            }));
        }

        [Fact]
        public async Task UpdateEntrepriseContact_FreeLancerNoVat_ThrowsException()
        {
            //Arrange
            Mock<IEntrepriseContactStorageManager> entrepriseContactStorageManager = new Mock<IEntrepriseContactStorageManager>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IEntrepriseStorageManager> entrepriseStorageManager = new Mock<IEntrepriseStorageManager>();
            Mock<IContactStorageManager> contactStorageManager = new Mock<IContactStorageManager>();

            EntrepriseContactManager contactManager = new EntrepriseContactManager(entrepriseContactStorageManager.Object, mapper.Object, entrepriseStorageManager.Object, contactStorageManager.Object);
            entrepriseContactStorageManager.Setup(x => x.GetEntrepriseContact(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new EntrepriseContact { 
                ContactId = 1,
                EntrepriseId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Employee
            });

            //Act


            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => contactManager.UpdateEntrepriseContact(new EntrepriseContactModel
            {
                ContactId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Freelancer,
                EntrepriseId = 1
            }));
        }

        [Fact]
        public async Task UpdateEntrepriseContact_AllOk_NoErrorsAndUpdateFunctionCalled()
        {
            //Arrange
            Mock<IEntrepriseContactStorageManager> entrepriseContactStorageManager = new Mock<IEntrepriseContactStorageManager>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<IEntrepriseStorageManager> entrepriseStorageManager = new Mock<IEntrepriseStorageManager>();
            Mock<IContactStorageManager> contactStorageManager = new Mock<IContactStorageManager>();

            EntrepriseContactManager contactManager = new EntrepriseContactManager(entrepriseContactStorageManager.Object, mapper.Object, entrepriseStorageManager.Object, contactStorageManager.Object);
            EntrepriseContact entrepriseContact = new EntrepriseContact
            {
                ContactId = 1,
                EntrepriseId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Employee
            };
            entrepriseContactStorageManager.Setup(x => x.GetEntrepriseContact(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(entrepriseContact);

            //Act
            await contactManager.UpdateEntrepriseContact(new EntrepriseContactModel
            {
                ContactId = 1,
                ContactType = ContactManagementService.Enums.EContactType.Freelancer,
                EntrepriseId = 1,
                VATNumber = "123456"
            });

            //Assert
            entrepriseContactStorageManager.Verify(x => x.UpdateEntrepriseContact(entrepriseContact), Times.Once);
        }
    }
}
