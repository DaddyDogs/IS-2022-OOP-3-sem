using ApplicationLayer.Dto;
using DataAccessLayer.Models;

namespace ApplicationLayer.Mapping;

public static class PrivateAccountDtoMapping
{
    public static PrivateAccountDto AsDto(this PrivateAccount privateAccount) =>
        new PrivateAccountDto(privateAccount.Password, privateAccount.Login, privateAccount.Accounts.Select(x => x.AsDto()).ToList(), privateAccount.Employee.Identifier, privateAccount.Id);
}