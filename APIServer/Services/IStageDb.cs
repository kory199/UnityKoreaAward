﻿using APIServer.DbModel;

namespace APIServer.Services;

public interface IStageDb
{
    public Task<(ResultCode, Stage?)> CreateDefaultStageData(Int64 account_id);

    public Task<(ResultCode, Int32?)> VerifyStageAsync(Int64 account_id);

    public Task<(ResultCode, Int32)> UpdataStageAsync(Int64 account_id, Int32 stage_id );
}