using Microsoft.EntityFrameworkCore;
using WebApplicationCF.DTOs;
using WebApplicationCF.Infrastracture;
using WebApplicationCF.Models;

namespace WebApplicationCF.Services;

public class PCsService(DatabaseContext ctx) : IPCsService
{
    public async Task<IEnumerable<PCResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var pcs = await ctx.PCs
            .OrderBy(pc => pc.Id)
            .Select(pc => new PCResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync(cancellationToken);

        foreach (var pc in pcs)
        {
            pc.Weight = Math.Round(pc.Weight, 1);
        }

        return pcs;
    }

    public async Task<PCDetailsResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var pc = await ctx.PCs
            .Where(pc => pc.Id == id)
            .Select(pc => new PCDetailsResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock,
                Components = pc.PCComponents
                    .OrderBy(pcComponent => pcComponent.ComponentCode)
                    .Select(pcComponent => new PCComponentResponseDto
                    {
                        Amount = pcComponent.Amount,
                        Component = new ComponentResponseDto
                        {
                            Code = pcComponent.Component.Code,
                            Name = pcComponent.Component.Name,
                            Description = pcComponent.Component.Description,
                            Manufacturer = new ComponentManufacturerResponseDto
                            {
                                Id = pcComponent.Component.ComponentManufacturer.Id,
                                Abbreviation = pcComponent.Component.ComponentManufacturer.Abbreviation,
                                FullName = pcComponent.Component.ComponentManufacturer.FullName,
                                FoundationDate = pcComponent.Component.ComponentManufacturer.FoundationDate
                            },
                            Type = new ComponentTypeResponseDto
                            {
                                Id = pcComponent.Component.ComponentType.Id,
                                Abbreviation = pcComponent.Component.ComponentType.Abbreviation,
                                Name = pcComponent.Component.ComponentType.Name
                            }
                        }
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (pc is not null)
        {
            pc.Weight = Math.Round(pc.Weight, 1);
        }

        return pc;
    }

    public async Task<PCResponseDto> AddAsync(CreatePCRequestDto request, CancellationToken cancellationToken)
    {
        var pc = new PC
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        ctx.PCs.Add(pc);
        await ctx.SaveChangesAsync(cancellationToken);

        return new PCResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = Math.Round(pc.Weight, 1),
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdatePCRequestDto request,
        CancellationToken cancellationToken)
    {
        var pc = await ctx.PCs.FirstOrDefaultAsync(pc => pc.Id == id, cancellationToken);

        if (pc is null)
        {
            return false;
        }

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await ctx.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var pc = await ctx.PCs
            .Include(pc => pc.PCComponents)
            .FirstOrDefaultAsync(pc => pc.Id == id, cancellationToken);

        if (pc is null)
        {
            return false;
        }

        ctx.PCComponents.RemoveRange(pc.PCComponents);
        ctx.PCs.Remove(pc);
        await ctx.SaveChangesAsync(cancellationToken);

        return true;
    }
}
