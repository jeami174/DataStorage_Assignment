﻿using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, IServiceRepository serviceRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<bool> CreateProjectAsync(ProjectCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return false;

        var exists = await _projectRepository.ExistsAsync(x => x.Title.ToLower() == dto.Title.ToLower());
        if (exists)
            return false;

        var serviceEntity = await _serviceRepository.GetOneAsync(s => s.Id == dto.ServiceId);
        if (serviceEntity == null)
            return false;

        await _projectRepository.BeginTransactionAsync();

        try
        {
            var projectEntity = ProjectFactory.CreateProjectEntity(dto, serviceEntity.PricePerUnit);
            await _projectRepository.CreateAsync(projectEntity);
            await _projectRepository.SaveToDatabaseAsync();
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating project entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectUpdateDto dto)
    {
        var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
        if (entity == null)
            return false;

        ProjectFactory.UpdateProjectEntity(entity, dto);

        await _projectRepository.BeginTransactionAsync();

        try
        {
            _projectRepository.Update(entity);
            await _projectRepository.SaveToDatabaseAsync();
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating project entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _projectRepository.BeginTransactionAsync();

        try
        {
            _projectRepository.Delete(entity);
            await _projectRepository.SaveToDatabaseAsync();
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting project entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<ProjectModel>> GetAllProjectsWithDetailsAsync()
    {
        var entities = await _projectRepository.GetAllWithDetailsAsync(query =>
            query.Include(p => p.Customer)
                 .ThenInclude(c => c.Contacts)
                 .Include(p => p.Service)
                 .ThenInclude(s => s.Unit)
                 .Include(p => p.User)
                 .ThenInclude(u => u.Role)
                 .Include(p => p.Status));
        return entities.Select(ProjectFactory.CreateProjectModel).ToList();
    }

    public async Task<ProjectModel?> GetProjectByIdAsync(int id)
    {
        var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
        return entity != null ? ProjectFactory.CreateProjectModel(entity) : null;
    }

    public async Task<ProjectModel?> GetProjectWithDetailsByIdAsync(int id)
    {
        var entity = await _projectRepository.GetOneWithDetailsAsync(query =>
            query.Include(p => p.Customer)
                 .ThenInclude(c => c.Contacts)
                 .Include(p => p.Service)
                 .ThenInclude(s => s.Unit)
                 .Include(p => p.User)
                 .ThenInclude(u => u.Role)
                 .Include(p => p.Status),
            x => x.Id == id);

        return entity != null ? ProjectFactory.CreateProjectModel(entity) : null;
    }

    public async Task<bool> CheckIfProjectExists(Expression<Func<ProjectEntity, bool>> predicate)
    {
        return await _projectRepository.ExistsAsync(predicate);
    }
}

