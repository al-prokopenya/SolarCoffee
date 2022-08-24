using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using SolarCoffee.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SolarCoffee.Data.Models;
using FluentAssertions;
using System.Linq;
using Moq;

namespace SolarCoffee.Tests
{
    
    public class TestCustomerService
    {
        [Fact]
        public void CustomerService_GetsAllCustomers_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<SolarDbContext>()
                .UseInMemoryDatabase("gets_all").Options;

            using var context = new SolarDbContext(options);

            var service = new CustomerService(context);

            var res1 = service.CreateCustomer(
                new Customer 
                { 
                    Id = 1234,
                    Name = "Test",
                    LastName = "Test",
                    PrimaryAddress = new CustomerAddress { 
                        AddressLine1 = "test", AddressLine2 = "test", ZipCode = "121243", City = "Any", Country = "AA", State = "ST",
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                    }
                });

            var res2 = service.CreateCustomer(
                new Customer 
                { 
                    Id = 1111,
                    Name = "Test",
                    LastName = "Test",
                    PrimaryAddress = new CustomerAddress
                    {
                        AddressLine1 = "test",
                        AddressLine2 = "test",
                        ZipCode = "121243",
                        City = "Any",
                        Country = "AA",
                        State = "ST",
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                    }
                });

            var allCustomers = service.GetAllCustomers();
            allCustomers.Count.Should().Be(2);
        }

        [Fact]
        public void CustomerService_CreateCustomer_GivenNewCustomerObject()
        {
            var options = new DbContextOptionsBuilder<SolarDbContext>()
                .UseInMemoryDatabase("create_one").Options;

            using var context = new SolarDbContext(options);

            var sut = new CustomerService(context);

            var res1 = sut.CreateCustomer(
                new Customer
                {
                    Id = 1234,
                    Name = "Test",
                    LastName = "Test",
                    PrimaryAddress = new CustomerAddress
                    {
                        AddressLine1 = "test",
                        AddressLine2 = "test",
                        ZipCode = "121243",
                        City = "Any",
                        Country = "AA",
                        State = "ST",
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                    }
                });

            context.Customers.Single().Id.Should().Be(1234);
        }

        [Fact]
        public void CustomerService_DeleteCustomer()
        {
            var options = new DbContextOptionsBuilder<SolarDbContext>()
                .UseInMemoryDatabase("delete_one").Options;

            using var context = new SolarDbContext(options);

            var sut = new CustomerService(context);

            var res1 = sut.CreateCustomer(
                new Customer
                {
                    Id = 1234,
                    Name = "Test",
                    LastName = "Test",
                    PrimaryAddress = new CustomerAddress
                    {
                        AddressLine1 = "test",
                        AddressLine2 = "test",
                        ZipCode = "121243",
                        City = "Any",
                        Country = "AA",
                        State = "ST",
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                    }
                });

            sut.DeleteCustomer(1234);

            sut.GetAllCustomers().Count.Should().Be(0);
        }

        [Fact]
        public void CustomerService_GetAllCustomersOrderedByLastName()
        {
            var data = new List<Customer> { 
                new Customer{ Id = 12, LastName="Zuzz"},
                new Customer{ Id = 23, LastName="Alpha"},
                new Customer{ Id = 234, LastName="Xman"},
                new Customer{ Id = 432, LastName="Lin"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            var mockContext = new Mock<SolarDbContext>();

            mockContext.Setup(c=>c.Customers).Returns(mockSet.Object);

            var sut = new CustomerService(mockContext.Object);

            //Action
            var customers = sut.GetAllCustomers();
            customers.Count.Should().Be(4);
            customers[0].Id.Should().Be(23);
            customers[1].Id.Should().Be(432);
            customers[2].Id.Should().Be(234);
            customers[3].Id.Should().Be(12);
        }
    }
}
