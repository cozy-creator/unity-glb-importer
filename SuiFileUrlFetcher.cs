using Newtonsoft.Json;
using Suinet.Rpc.Types;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class SuiFileUrlFetcher
{
    public static async Task<string> GetNftFileUrlAsync(string nftObjectId)
    {
        var getObjectRpcResult = await SuiApi.Client.GetObjectAsync(nftObjectId);

        if (!getObjectRpcResult.IsSuccess) return null;

        var obj = getObjectRpcResult.Result.Object;
        if (obj != null)
        {
            string fileUrl = (string)obj.Data.Fields["file_url"];
            return fileUrl;
        }

        return null;
    }
}