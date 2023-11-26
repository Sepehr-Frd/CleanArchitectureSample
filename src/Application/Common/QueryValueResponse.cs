#region

using System.Net;

#endregion

namespace Application.Common;

public record QueryValueResponse<T>(
    T Data,
    bool IsSuccessful = false,
    string Message = "",
    HttpStatusCode HttpStatusCode = HttpStatusCode.BadRequest)
    where T : struct;