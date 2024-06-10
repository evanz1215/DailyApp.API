using AutoMapper;
using DailyApp.API.ApiResponses;
using DailyApp.API.DataModel;
using DailyApp.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly DailyDbContext _db;
    private readonly IMapper _mapper;

    public AccountController(DailyDbContext context, IMapper mapper)
    {
        _db = context;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AccountInfoDto accountInfoDto)
    {
        ApiResponse response = new ApiResponse();

        try
        {
            var dbAccount = await _db.AccountInfo.Where(x => x.Account == accountInfoDto.Account).FirstOrDefaultAsync();

            if (dbAccount != null)
            {
                // 帳號已經存在
                response.ResultCode = -1; // 帳號已經存在
                response.Msg = "帳號已經存在";
                return BadRequest(response);
            }

            var accountInfo = _mapper.Map<AccountInfo>(accountInfoDto);

            //AccountInfo accountInfo = new AccountInfo()
            //{
            //    Name = accountInfoDto.Name,
            //    Account = accountInfoDto.Account,
            //    Pwd = accountInfoDto.Password
            //};

            _db.AccountInfo.Add(accountInfo);

            int result = await _db.SaveChangesAsync();

            if (result == 1)
            {
                response.ResultCode = 1; // 帳號註冊成功
                response.Msg = "帳號註冊成功";
                return Ok(response);
            }
            else
            {
                response.ResultCode = -99; // 失敗
                response.Msg = "帳號註冊失敗";
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            response.ResultCode = -99; // 失敗
            response.Msg = "帳號註冊失敗";
            return BadRequest(response);
        }
    }

    [HttpGet("login")]
    public async Task<IActionResult> LoginAsync(string account, string password)
    {
        ApiResponse apiResponse = new ApiResponse();

        // 檢查是否輸入
        if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
        {
            apiResponse.ResultCode = -1; // 登入失敗
            apiResponse.Msg = "登入失敗";
            return BadRequest(apiResponse);
        }

        var dbAccount = await _db.AccountInfo.Where(x => x.Account == account && x.Pwd == password).FirstOrDefaultAsync();

        if (dbAccount == null)
        {
            apiResponse.ResultCode = -1; // 登入失敗
            apiResponse.Msg = "登入失敗";
            return BadRequest(apiResponse);
        }

        apiResponse.ResultCode = 1; // 登入成功
        apiResponse.Msg = "登入成功";
        apiResponse.ResultData = dbAccount;
        return Ok(apiResponse);
    }
}