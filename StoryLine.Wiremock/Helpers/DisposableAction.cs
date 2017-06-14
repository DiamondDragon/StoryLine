using System;

namespace StoryLine.Wiremock.Helpers
{
    public class DisposableAction : IDisposable
    {
        private readonly Action _onDispose;

        public DisposableAction(Action onDispose)
        {
            if (onDispose == null)
                throw new ArgumentNullException(nameof(onDispose));

            _onDispose = onDispose;
        }

        public void Dispose()
        {
            _onDispose();
        }
    }
}