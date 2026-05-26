using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Services;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Entitys;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RestApiModeloDDD.Tests
{
    [TestFixture]
    public class ApplicationServiceClienteTests
    {
        private Fixture _fixture;

        private Mock<IServiceCliente> _serviceClienteMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<ApplicationServiceCliente>> _loggerMock;

        private ApplicationServiceCliente _applicationServiceCliente;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _serviceClienteMock =
                new Mock<IServiceCliente>();

            _mapperMock =
                new Mock<IMapper>();

            _loggerMock =
                new Mock<ILogger<ApplicationServiceCliente>>();

            _applicationServiceCliente =
                new ApplicationServiceCliente(
                    _serviceClienteMock.Object,
                    _mapperMock.Object,
                    _loggerMock.Object);
        }

        [Test]
        public async Task ApplicationServiceCliente_GetAll_ShouldReturnFiveClients()
        {
            // Arrange

            var clientes = _fixture
                .Build<Cliente>()
                .CreateMany(5)
                .ToList();

            var clientesDto = _fixture
                .Build<ClienteDto>()
                .CreateMany(5)
                .ToList();

            _serviceClienteMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(clientes);

            _mapperMock
                .Setup(x => x.Map<IEnumerable<ClienteDto>>(clientes))
                .Returns(clientesDto);

            // Act

            var result =
                await _applicationServiceCliente.GetAllAsync();

            // Assert

            Assert.IsNotNull(result);

            Assert.AreEqual(
                5,
                result.Count());

            _serviceClienteMock.VerifyAll();

            _mapperMock.VerifyAll();
        }

        [Test]
        public async Task ApplicationServiceCliente_GetById_ShouldReturnClient()
        {
            // Arrange

            const int id = 10;

            var cliente = _fixture
                .Build<Cliente>()
                .With(c => c.Id, id)
                .With(c => c.Email, "teste1@teste.com.br")
                .Create();

            var clienteDto = _fixture
                .Build<ClienteDto>()
                .With(c => c.Id, id)
                .With(c => c.Email, "teste1@teste.com.br")
                .Create();

            _serviceClienteMock
                .Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(cliente);

            _mapperMock
                .Setup(x => x.Map<ClienteDto>(cliente))
                .Returns(clienteDto);

            // Act

            var result =
                await _applicationServiceCliente.GetByIdAsync(id);

            // Assert

            Assert.IsNotNull(result);

            Assert.AreEqual(
                "teste1@teste.com.br",
                result.Email);

            Assert.AreEqual(
                10,
                result.Id);

            _serviceClienteMock.VerifyAll();

            _mapperMock.VerifyAll();
        }
    }
}