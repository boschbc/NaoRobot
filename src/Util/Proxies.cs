using System;
using System.Collections;
using System.Collections.Generic;
using Aldebaran.Proxies;
using Naovigate.Communication;

namespace Naovigate.Util
{
    /// <summary>
    /// proxy manager for nao proxies
    /// </summary>
    public static class Proxies
    {
        private static Action[] unsubscribers;
        static Proxies()
        {
            unsubscribers = new Action[3] 
            { 
                UnsubscribeLandMarkProxies, 
                UnsubscribeSensorProxies, 
                UnsubscribeSonarProxies 
            };
        }
        private static ISet<IDisposable> proxies = new HashSet<IDisposable>();

        /// <summary>
        /// Attempts to create a proxy of given type.
        /// </summary>
        /// <typeparam name="TProxy">The type of proxy to be created.</typeparam>
        /// <returns>A new proxy of the requested type.</returns>
        /// <exception cref="UnavailableConnectionException">Proxy creation failed.</exception>
        public static TProxy GetProxy<TProxy>() where TProxy : IDisposable
        {
            try
            {
                NaoState state = NaoState.Instance;
                TProxy ret = (TProxy)Activator.CreateInstance(typeof(TProxy), state.IP.ToString(), state.Port);
                proxies.Add(ret);
                return ret;
            }
            catch
            {
                throw new UnavailableConnectionException("Could not create proxy: " + typeof(TProxy).Name);
            }
        }

        /// <summary>
        /// Dispose all the procies in use.
        /// </summary>
        public static void DisposeAllProxies()
        {
            try
            {
                foreach (IDisposable d in proxies)
                {
                    Logger.Log(typeof(Proxies), "Disposing of: " + d);
                    d.Dispose();
                }
            }
            catch
            {
                proxies.Clear();
                throw new UnavailableConnectionException("Error while disconnecting proxies.");
            }
            proxies.Clear();
        }

        /// <summary>
        /// Unsubscribes from any landmark-detection proxies.
        /// </summary>
        private static void UnsubscribeLandMarkProxies()
        {
            LandMarkDetectionProxy landmark = Proxies.GetProxy<LandMarkDetectionProxy>();
            foreach (ArrayList sub in (ArrayList)landmark.getSubscribersInfo())
            {
                landmark.unsubscribe(sub[0].ToString());
            }
        }

        /// <summary>
        /// Unsubscribes from any sensor proxies.
        /// </summary>
        private static void UnsubscribeSensorProxies()
        {
            SensorsProxy sensors = GetProxy<SensorsProxy>();
            foreach (ArrayList sub in (ArrayList)sensors.getSubscribersInfo())
            {
                sensors.unsubscribe(sub[0].ToString());
            }
        }

        /// <summary>
        /// Unsubscribes from any sonar proxies.
        /// </summary>
        private static void UnsubscribeSonarProxies()
        {
            SonarProxy sonar = Proxies.GetProxy<SonarProxy>();
            foreach (ArrayList sub in (ArrayList)sonar.getSubscribersInfo())
            {
                sonar.unsubscribe(sub[0].ToString());
            }
        }

        /// <summary>
        /// Unsubscribes all instances of LandMarkDetectionProxy, SensorsProxy and SonarProxy.
        /// </summary>
        /// <returns>Whether all subscriptions were undone.</returns>
        public static bool UnsubscribeAll()
        {
            bool noError = true;
            foreach (Action unsub in unsubscribers)
            {
                try
                {
                    unsub();
                }
                catch (UnavailableConnectionException)
                {
                    noError = false;
                }
            }
            return noError;
        }
    }
}
