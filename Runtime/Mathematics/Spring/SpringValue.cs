﻿using UnityEngine;

namespace Lab5Games.Mathematics
{
    /// <summary>
    /// https://github.com/llamacademy/juicy-springs/tree/main/Assets/Scripts/Spring
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SpringValue<T>
    {
        // Default to critically damped
        public virtual float Damping { get; set; } = 26f;
        public virtual float Mass { get; set; } = 1f;
        public virtual float Stiffness { get; set; } = 169f;
        public virtual T StartValue { get; set; }
        public virtual T EndValue { get; set; }
        public virtual T InitialVelocity { get; set; }
        public virtual T CurrentValue { get; set; }
        public virtual T CurrentVelocity { get; set; }

        /// <summary>
        /// Reset all values to initial states.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Update the end value in the middle of motion.
        /// This reuse the current velocity and interpolate the value smoothly afterwards.
        /// </summary>
        /// <param name="Value">End value</param>
        public virtual void UpdateEndValue(T Value) => UpdateEndValue(Value, CurrentVelocity);

        /// <summary>
        /// Update the end value in the middle of motion but using a new velocity.
        /// </summary>
        /// <param name="Value">End value</param>
        /// <param name="Velocity">New velocity</param>
        public abstract void UpdateEndValue(T Value, T Velocity);

        /// <summary>
        /// Advance a step by deltaTime(seconds).
        /// </summary>
        /// <param name="DeltaTime">Delta time since previous frame</param>
        /// <returns>Evaluated value</returns>
        public abstract T Evaluate(float DeltaTime);
    }
}
