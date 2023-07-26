public interface IPayloadedState<TPayload> : IUpdatableState
{
    void Enter(TPayload payload);
}