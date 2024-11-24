using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Tests;

public static class TestHelper
{
    public static IRepository CreateRepository<T>(IEnumerable<T> entities) where T : class
    {
        var mockContext = new Mock<DbContext>();
        var mockRepository = new Mock<IRepository>();
        var entity = entities.First();
        var entityToUpdate = entities.Skip(1).FirstOrDefault();

        mockContext.Setup(ctx => ctx.Set<T>()).ReturnsDbSet(entities);
        mockRepository.Setup(repo => repo.GetAll<T>()).Returns(mockContext.Object.Set<T>());
        mockRepository.Setup(repo => repo.GetById<T>(1)).ReturnsAsync(entity);
        mockRepository.Setup(repo => repo.Add(entity)).ReturnsAsync(entity);
        mockRepository.Setup(repo => repo.Update(entity)).ReturnsAsync(entityToUpdate);

        return mockRepository.Object;
    }
}