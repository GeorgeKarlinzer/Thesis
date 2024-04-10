using CryptLearn.Modules.ModuleManagement.Core.DTOs;
using CryptLearn.Modules.ModuleManagement.Core.Entities;
using CryptLearn.Modules.ModuleManagement.Core.Exceptions;
using CryptLearn.Modules.ModuleManagement.Core.Services;
using CryptLearn.Modules.ModuleManagement.Unit.Moqs;
using FluentAssertions;

namespace CryptLearn.Modules.ModuleManagement.Unit.Services
{
    internal class ModuleManagementService_Test
    {
        private readonly InMemoryUsersRepository _inMemoryUsersRepository;
        private readonly InMemoryModulesRepository _inMemoryModulesRepository;
        private readonly ModuleManagementService _service;

        public ModuleManagementService_Test()
        {
            _inMemoryUsersRepository = new InMemoryUsersRepository();
            _inMemoryModulesRepository = new InMemoryModulesRepository();
            _service = new ModuleManagementService(_inMemoryModulesRepository, _inMemoryUsersRepository);
        }

        [SetUp]
        public void SetUp()
        {
            _inMemoryModulesRepository.Modules.Clear();
            _inMemoryUsersRepository.Users.Clear();
            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "User1"
            };
            _inMemoryUsersRepository.Users.Add(user);
        }

        [TestCase(TestName = "Browse modules correctly")]
        public async Task BrowseTest()
        {
            // arrange
            var module = new Module()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Description = "Description",
                AuthorId = _inMemoryUsersRepository.Users.First().Id,
            };
            _inMemoryModulesRepository.Modules.Add(module);
            _inMemoryModulesRepository.Modules.Add(module);
            _inMemoryModulesRepository.Modules.Add(module);
            _inMemoryModulesRepository.Modules.Add(module);

            // act
            var result = await _service.BrowseAsync();

            // assert
            result.Should().HaveCount(4);
        }

        [TestCase(TestName = "Get existing module")]
        public async Task GetExistingModule()
        {
            // arrange
            var id = Guid.NewGuid();
            var module = new Module()
            {
                Id = id,
                Name = "Name",
                Description = "Description",
                AuthorId = _inMemoryUsersRepository.Users.First().Id,
            };
            _inMemoryModulesRepository.Modules.Add(module);

            // act
            var result = await _service.GetAsync(id);

            // assert
            result.Should().NotBeNull();
        }

        [TestCase(TestName = "Get non-existing module")]
        public async Task GetNonExistingModule()
        {
            // arrange
            var id = Guid.NewGuid();

            // act
            var a = await _service.GetAsync(id);

            // assert
            a.Should().BeNull();
        }

        [TestCase(TestName = "Create module")]
        public async Task CreateModule()
        {
            // arrange
            var dto = new CreateModuleDto("Name", "Description");

            // act
            await _service.CreateModule(dto, Guid.NewGuid());

            // assert
            _inMemoryModulesRepository.Modules.Should().HaveCount(1);
        }

        [TestCase(TestName = "Delete existing module")]
        public async Task DeleteExistingModule()
        {
            // arrange
            var id = Guid.NewGuid();
            var module = new Module()
            {
                Id = id,
                Name = "Name",
                Description = "Description",
                AuthorId = _inMemoryUsersRepository.Users.First().Id,
            };
            _inMemoryModulesRepository.Modules.Add(module);

            // act
            await _service.DeleteModule(id);

            // assert
            _inMemoryModulesRepository.Modules.Should().HaveCount(0);
        }

        [TestCase(TestName = "Delete non-existing module")]
        public async Task DeleteNonExistingModule()
        {
            // arrange
            var id = Guid.NewGuid();
            
            // act
            var a = async () => await _service.DeleteModule(id);

            // assert
            await a.Should().ThrowExactlyAsync<ModuleNotFoundException>();
        }

        [TestCase(TestName = "Update existing module")]
        public async Task UpdateExistingModule()
        {
            // arrange
            var id = Guid.NewGuid();
            var module = new Module()
            {
                Id = id,
                Name = "Name",
                Description = "Description",
                AuthorId = _inMemoryUsersRepository.Users.First().Id,
            };
            _inMemoryModulesRepository.Modules.Add(module);
            var dto = new UpdateModuleDto()
            {
                Id = id,
                Name = "Name1",
                Description = "Description"
            };

            // act
            await _service.UpdateModule(dto);

            // assert
            _inMemoryModulesRepository.Modules.First().Name.Should().Be("Name1");
        }

        [TestCase(TestName = "Update non-existing module")]
        public async Task UpdateNonExistingModule()
        {
            // arrange
            var id = Guid.NewGuid();
            var dto = new UpdateModuleDto()
            {
                Id = id
            };

            // act
            var a = async () => await _service.UpdateModule(dto);

            // assert
            await a.Should().ThrowExactlyAsync<ModuleNotFoundException>();
        }
    }
}
