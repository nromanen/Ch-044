using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL;
using BAL.Manager;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using Moq;
using NUnit.Framework;

namespace WSDP.Tests.BAL {
	[TestFixture]
	public class PropertyManager_Tests 
	{
		[Test]
		public void PropertyManager_Get() 
		{
			AutoMapperConfig.Configure();
			// mock Repo logic
			var propRepo = new Mock<IGenericRepository<Property>>();
			var uof = new Mock<IUnitOfWork>();
			uof.Setup(x => x.PropertyRepo).Returns(propRepo.Object);

			var mngr = new PropertyManager(uof.Object);

			var dbStub = new Property() 
			{
				Name = "Display"
			};

			var stub = new PropertyDTO() 
			{
				Name = "Display"
			};

			propRepo.Setup(x => x.GetByID(It.IsAny<int>())).Returns(dbStub);
			var result = mngr.Get(2);
			Assert.IsNotNull(result, "Null property returned");
			Assert.AreEqual(stub.Name, result.Name);
		}
	}
}
