using Moq;
using Xunit;

namespace BankingApi.Tests;

public class AccountServiceTests
{
    [Fact]
    public void GetAccountById_ReturnsAccount_WhenExists()
    {
        // Arrange
        var mockRepo = new Mock<IAccountRepository>();
        var account = new Account { Id = 1, Owner = "Ahmet", Balance = 1000m };
        mockRepo.Setup(r => r.GetById(1)).Returns(account);
        var service = new AccountService(mockRepo.Object);

        // Act
        var result = service.GetAccountById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Ahmet", result.Owner);
    }

    [Fact]
    public void GetAccountById_ReturnsNull_WhenNotFound()
    {
        var mockRepo = new Mock<IAccountRepository>();
        mockRepo.Setup(r => r.GetById(99)).Returns((Account?)null);
        var service = new AccountService(mockRepo.Object);

        var result = service.GetAccountById(99);

        Assert.Null(result);
    }

    [Fact]
    public void UpdateAccount_ReturnsNull_WhenAccountDoesNotExist()
    {
        var mockRepo = new Mock<IAccountRepository>();
        mockRepo.Setup(r => r.GetById(5)).Returns((Account?)null);
        var service = new AccountService(mockRepo.Object);

        var result = service.UpdateAccount(5, new Account { Owner = "Zeynep", Balance = 500m });

        Assert.Null(result);
    }

    [Fact]
    public void UpdateAccount_UpdatesOwnerAndBalance_WhenAccountExists()
    {
        var mockRepo = new Mock<IAccountRepository>();
        var existing = new Account { Id = 2, Owner = "Eski İsim", Balance = 100m };
        mockRepo.Setup(r => r.GetById(2)).Returns(existing);
        var service = new AccountService(mockRepo.Object);

        var result = service.UpdateAccount(2, new Account { Owner = "Yeni İsim", Balance = 999m });

        Assert.NotNull(result);
        Assert.Equal("Yeni İsim", result.Owner);
        Assert.Equal(999m, result.Balance);
    }

    [Fact]
    public void Deposit_IncreasesBalance_WhenAccountExists()
    {
        var mockRepo = new Mock<IAccountRepository>();
        var existing = new Account { Id = 3, Owner = "Mehmet", Balance = 1000m };
        mockRepo.Setup(r => r.GetById(3)).Returns(existing);
        var service = new AccountService(mockRepo.Object);

        var result = service.Deposit(3, 500m);

        Assert.NotNull(result);
        Assert.Equal(1500m, result.Balance);
    }

    [Fact]
    public void DeleteAccount_ReturnsFalse_WhenAccountDoesNotExist()
    {
        var mockRepo = new Mock<IAccountRepository>();
        mockRepo.Setup(r => r.GetById(10)).Returns((Account?)null);
        var service = new AccountService(mockRepo.Object);

        var result = service.DeleteAccount(10);

        Assert.False(result);
    }
}