using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;
        public TenantRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<CreateTenantDto>> GetByOwnerIdAsync(int ownerId) =>
            await _context.Tenants
                .Where(t => t.OwnerId == ownerId)
                .Select(t => new CreateTenantDto
                {
                    OwnerId = t.OwnerId,
                    TenantName = t.TenantName,
                    PrimaryContactNumber = t.PrimaryContactNumber,
                    FamilyStatus = t.FamilyStatus,
                    MembersCount = t.MembersCount,
                    MonthlyRent = t.MonthlyRent,
                    PermanentAddress = t.PermanentAddress,
                    ProfileImageUrl = t.ProfileImageUrl,
                    EmergencyContact = t.EmergencyContact,
                    VehicleDetails = t.VehicleDetails,
                    AadhaarNumber = t.AadhaarNumberEncrypted
                })
                .ToListAsync();

        public async Task<TenantDto?> GetByIdAsync(int id) =>
            await _context.Tenants
                .Where(t => t.Id == id)
                .Select(t => new TenantDto
                {
                    Id = t.Id,
                    TenantName = t.TenantName,
                    PrimaryContactNumber = t.PrimaryContactNumber,
                    FamilyStatus = t.FamilyStatus,
                    MembersCount = t.MembersCount,
                    MonthlyRent = t.MonthlyRent,
                    PermanentAddress = t.PermanentAddress,
                    ProfileImageUrl = t.ProfileImageUrl,
                    EmergencyContact = t.EmergencyContact,
                    VehicleDetails = t.VehicleDetails
                })
                .FirstOrDefaultAsync();

        public async Task AddAsync(CreateTenantDto tenantDto)
        {
            var tenant = new Tenants
            {
                OwnerId = tenantDto.OwnerId,
                TenantName = tenantDto.TenantName,
                PrimaryContactNumber = tenantDto.PrimaryContactNumber,
                FamilyStatus = tenantDto.FamilyStatus,
                MembersCount = tenantDto.MembersCount,
                MonthlyRent = tenantDto.MonthlyRent,
                PermanentAddress = tenantDto.PermanentAddress,
                ProfileImageUrl = tenantDto.ProfileImageUrl,
                EmergencyContact = tenantDto.EmergencyContact,
                VehicleDetails = tenantDto.VehicleDetails,
                AadhaarNumberEncrypted = tenantDto.AadhaarNumber
            };
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TenantDto tenantDto)
        {
            var tenant = await _context.Tenants.FindAsync(tenantDto.Id);
            if (tenant != null)
            {
                tenant.TenantName = tenantDto.TenantName;
                tenant.PrimaryContactNumber = tenantDto.PrimaryContactNumber;
                tenant.FamilyStatus = tenantDto.FamilyStatus;
                tenant.MembersCount = tenantDto.MembersCount;
                tenant.MonthlyRent = tenantDto.MonthlyRent;
                tenant.PermanentAddress = tenantDto.PermanentAddress;
                tenant.ProfileImageUrl = tenantDto.ProfileImageUrl;
                tenant.EmergencyContact = tenantDto.EmergencyContact;
                tenant.VehicleDetails = tenantDto.VehicleDetails;

                _context.Tenants.Update(tenant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant != null)
            {
                _context.Tenants.Remove(tenant);
                await _context.SaveChangesAsync();
            }
        }
    }
}
