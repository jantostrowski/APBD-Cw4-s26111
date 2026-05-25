using WebApplicationCF.DTOs;

namespace WebApplicationCF.Services;

public interface IPCsService
{
    Task<IEnumerable<PCResponseDto>> GetAllAsync(CancellationToken cancellationToken);

    Task<PCDetailsResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<PCResponseDto> AddAsync(CreatePCRequestDto request, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(int id, UpdatePCRequestDto request, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
