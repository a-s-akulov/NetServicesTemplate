﻿using AutoMapper;
using System.Diagnostics;
using $safeprojectname$.RequestHandlers.Base;
using $safeprojectname$.DTO.V1.MyAwesomeProducts;
using $ext_safeprojectname$Services.Common.Services.$ext_safeprojectname$StorageService;


namespace $safeprojectname$.RequestHandlers.V1.MyAwesomeProducts;


public class GetProductV1RequestHandler : RequestHandlerBase<I$ext_safeprojectname$StorageService, GetProductRequest, GetProductResponse>
{
    #region Конструкторы

    public GetProductV1RequestHandler(I$ext_safeprojectname$StorageService storageService, ILogger<GetProductV1RequestHandler> logger, IMapper mapper, ActivitySource activitySource)
                                                : base(storageService, logger, mapper, activitySource)
    {
    }

    #endregion Конструкторы


    #region Методы

    protected override ApiResponse<GetProductResponse>? ValidateRequest(GetProductRequest request)
    {
        List<ApiError>? validationErrors = null;
        if (request.Id == null)
            (validationErrors ??= []).Add(new ApiError()
            {
                Code = System.Net.HttpStatusCode.BadRequest.ToString(),
                Title = $"{nameof(request.Id)} is empty",
                Detail = $"{nameof(request.Id)} is required field",
                Source = nameof(GetProductRequest)
            });

        if (validationErrors != null && validationErrors.Count > 0)
            return new(null, System.Net.HttpStatusCode.BadRequest, errors: validationErrors);

        return null;
    }


    protected override async Task<GetProductResponse> HandleCore(GetProductRequest request, CancellationToken cancellationToken)
    {
        var results = await BaseService.GetMyAwesomeProducts(idsFilter: [request.Id], cancellationToken: cancellationToken).ConfigureAwait(false);
        var result = results.FirstOrDefault();

        if (result == null)
            throw new KeyNotFoundException($"Not found my awesome product with ID '{request.Id}'");

        return new();
    }


    protected override ApiResponse<GetProductResponse>? HandleException(GetProductRequest request, Exception exception)
    {
        Log.LogError(exception, "Failed to get my awesome product with ID '{MyAwesomeProductId}'", request.Id);
        if (exception is KeyNotFoundException)
            return new(null, System.Net.HttpStatusCode.NotFound, [exception.ToApiError(statusCode: System.Net.HttpStatusCode.NotFound)]);

        return null;
    }

    #endregion Методы
}
