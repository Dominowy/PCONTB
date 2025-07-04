using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Domain.Account.Sessions;
using PCONTB.Panel.Domain.Repositories;

namespace PCONTB.Panel.Application.Services.Auth
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Session?> GetByIdAsync(Guid? sessionId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.SessionRepository.GetBy(m => m.Id == sessionId, cancellationToken);
        }

        public async Task<Guid> CreateSession(Guid userId, CancellationToken cancellationToken)
        {
            var session = new Session(Guid.NewGuid(), userId);

            await _unitOfWork.SessionRepository.Add(session, cancellationToken);

            await _unitOfWork.Save(cancellationToken);

            return session.Id;
        }

        public async Task<bool> CheckSessionActiveState(Session session, CancellationToken cancellationToken)
        {
            if (session.Ended < DateTimeOffset.UtcNow)
            {
                session.EndSession();

                await _unitOfWork.SessionRepository.Update(session, cancellationToken);

                await _unitOfWork.Save(cancellationToken);
                return true;
            }

            return false;
        }

        public async Task EndAllSession(Guid userId, CancellationToken cancellationToken)
        {
            var sessions = await _unitOfWork.SessionRepository.GetAll(m => m.UserId == userId && m.Enabled, cancellationToken);

            foreach (var session in sessions)
            {
                session.EndSession();
                await _unitOfWork.SessionRepository.Update(session, cancellationToken);
            }

            await _unitOfWork.Save(cancellationToken);
        }
    }
}
