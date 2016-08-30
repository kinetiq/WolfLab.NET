using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Ether.Outcomes.Builder;

namespace EatVillagers.WolfLab.Logic.Analytics
{
    /// <summary>
    /// Singleton used to record gameplay statistics.
    /// </summary>
    public class StatsSingleton
    {
        public static readonly StatsSingleton Instance = new StatsSingleton();

        public List<Experiment> Experiments;
        public Experiment CurrentExperiment;
        private Stopwatch Stopwatch;
   
        public void StartNewExperiment(Experiment experiment)
        {
            if (Stopwatch.IsRunning || CurrentExperiment != null)
                CompleteExperiment();

            CurrentExperiment = experiment;

            this.Stopwatch = Stopwatch.StartNew();
        }

        public void CompleteExperiment()
        {
            var applyTags = new ApplyTagsService();

            if (!Stopwatch.IsRunning || CurrentExperiment == null)
                throw new InvalidOperationException("Cannot complete an experiment when no experiment is running.");

            Stopwatch.Stop();
            CurrentExperiment.Duration = Stopwatch.ElapsedMilliseconds;

            applyTags.Apply(CurrentExperiment);

            Experiments.Add(CurrentExperiment);
            CurrentExperiment = null;
        }

        private StatsSingleton()
        {
            Experiments = new List<Experiment>();
            Stopwatch = new Stopwatch();
        }
    }
}
