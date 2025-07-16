using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Services.Auth
{
    public class SessionService(IUnitOfWork unitOfWork) : ISessionService
    {
        public async Task<Session?> GetByIdAsync(Guid? sessionId, CancellationToken cancellationToken = default)
        {
            return await unitOfWork.SessionRepository.GetBy(m => m.Id == sessionId, cancellationToken);
        }

        public async Task<Guid> CreateSession(Guid userId, CancellationToken cancellationToken)
        {
            var session = new Session(Guid.NewGuid(), userId);

            await unitOfWork.SessionRepository.Add(session, cancellationToken);

            await unitOfWork.Save(cancellationToken);

            return session.Id;
        }

        public async Task<bool> CheckSessionActiveState(Session session, CancellationToken cancellationToken)
        {
            if (session.Ended < DateTimeOffset.UtcNow)
            {
                session.EndSession();

                await unitOfWork.SessionRepository.Update(session, cancellationToken);

                await unitOfWork.Save(cancellationToken);
                return true;
            }

            return false;
        }

        public async Task EndAllSession(Guid userId, CancellationToken cancellationToken)
        {
            var sessions = await unitOfWork.SessionRepository.GetAll(m => m.UserId == userId && m.Enabled, cancellationToken);

            foreach (var session in sessions)
            {
                session.EndSession();
                await unitOfWork.SessionRepository.Update(session, cancellationToken);
            }

            await unitOfWork.Save(cancellationToken);
        }
    }
}
