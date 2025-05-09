﻿using AutoMapper;
using System.Diagnostics;
using $safeprojectname$.RequestHandlers.Base;
using $safeprojectname$.DTO.V1.MyAwesomeProducts;
using $ext_safeprojectname$Services.Common.Services.$ext_safeprojectname$StorageService;


namespace $safeprojectname$.RequestHandlers.V1.MyAwesomeProducts;


public class DeleteProductV1RequestHandler : RequestHandlerBase<I$ext_safeprojectname$StorageService, DeleteProductRequest, DeleteProductResponse>
{
    #region Конструкторы

    public DeleteProductV1RequestHandler(I$ext_safeprojectname$StorageService storageService, ILogger<DeleteProductV1RequestHandler> logger, IMapper mapper, ActivitySource activitySource)
                                                : base(storageService, logger, mapper, activitySource)
    {
    }

    #endregion Конструкторы


    #region Методы

    protected override ApiResponse<DeleteProductResponse>? ValidateRequest(DeleteProductRequest request)
    {
        List<ApiError>? validationErrors = null;
        if (request.Id == null)
            (validationErrors ??= []).Add(new ApiError()
            {
                Code = System.Net.HttpStatusCode.BadRequest.ToString(),
                Title = $"{nameof(request.Id)} is empty",
                Detail = $"{nameof(request.Id)} is required field",
                Source = nameof(DeleteProductRequest)
            });

        if (validationErrors != null && validationErrors.Count > 0)
            return new(null, System.Net.HttpStatusCode.BadRequest, errors: validationErrors);

        return null;
    }


    protected override async Task<DeleteProductResponse> HandleCore(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var result = await BaseService.DeleteMyAwesomeProduct(request.Id, cancellationToken: cancellationToken).ConfigureAwait(false);

        if (!result)
            throw new KeyNotFoundException($"Not found my awesome product with ID '{request.Id}'");

        return new();
    }


    protected override ApiResponse<DeleteProductResponse>? HandleException(DeleteProductRequest request, Exception exception)
    {
        Log.LogError(exception, "Failed to delete my awesome product with ID '{MyAwesomeProductId}'", request.Id);
        if (exception is KeyNotFoundException)
            return new(null, System.Net.HttpStatusCode.NotFound, [exception.ToApiError(statusCode: System.Net.HttpStatusCode.NotFound)]);

        return null;
    }

    #endregion Методы
}
