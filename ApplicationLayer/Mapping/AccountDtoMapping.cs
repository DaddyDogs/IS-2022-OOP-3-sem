using ApplicationLayer.Dto;
using DataAccessLayer.Models;

namespace ApplicationLayer.Mapping;

public static class AccountDtoMapping
{
    public static AccountDto AsDto(this Account account) =>
        new AccountDto(account.MessageSource, account.Addressees.Select(x => x.AsDto()).ToList());
}