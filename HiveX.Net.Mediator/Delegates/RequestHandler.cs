

namespace HiveX.Net.Mediator.Delegates
{
    /// <summary>
    /// Delegate used in the pipeline to invoke the next handler or behavior.
    /// </summary>
    /// <typeparam name="TResponse">The type of the operation result.</typeparam>
    /// <returns>An asynchronous operation result as a Task.</returns>
    public delegate Task<TResponse> RequestHandler<TResponse>();

}
