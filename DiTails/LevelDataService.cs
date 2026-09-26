using BeatSaverSharp;
using BeatSaverSharp.Models;
using SiraUtil.Logging;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DiTails.Utilities;
using Zenject;

namespace DiTails
{
    internal class LevelDataService : ILateDisposable
    {
        private readonly SiraLog _siraLog;
        private readonly BeatSaver _beatSaver;

        internal LevelDataService(SiraLog siraLog)
        {
            _siraLog = siraLog;
            _beatSaver = new BeatSaver("DiTails", Assembly.GetExecutingAssembly().GetName().Version);
        }

        public void LateDispose()
        {
            _beatSaver.Clear();
            _beatSaver.Dispose();
        }

        internal async Task<Beatmap?> GetBeatmap(BeatmapLevel level, CancellationToken token)
        {
            if (level.TryGetHash(out var hash))
            {
                var beatmap = await _beatSaver.BeatmapByHash(hash, token);
                return beatmap ?? null;
            }

            return null;
        }
    }
}