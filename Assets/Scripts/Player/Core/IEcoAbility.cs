    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    namespace ECO.Player
    {
        public interface IEcoAbility
        {
            bool CanExecute();
            void Execute();
            void Stop();
            string GetAbilityName();
        }
    }