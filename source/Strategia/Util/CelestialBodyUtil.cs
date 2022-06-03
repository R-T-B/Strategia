using System;
using System.Collections.Generic;
using System.Linq;
using ContractConfigurator;
using Kopernicus.RuntimeUtility;

namespace Strategia
{
	public static class CelestialBodyUtil
	{
		public static IEnumerable<CelestialBody> GetBodiesForStrategy(string id)
		{
			CelestialBody home = (from cb in FlightGlobals.Bodies
			where cb.isHomeWorld
			select cb).Single<CelestialBody>();
			if (id == "MoonProgram")
			{
				foreach (CelestialBody child in home.orbitingBodies)
				{
					if (!child.name.Equals(RuntimeUtility.mockBody.name))
					{
						yield return child;
					}
				}
				List<CelestialBody>.Enumerator enumerator = default(List<CelestialBody>.Enumerator);
				if (home.referenceBody != FlightGlobals.Bodies[0])
				{
					IEnumerable<CelestialBody> orbitingBodies = home.referenceBody.orbitingBodies;
					Func<CelestialBody, bool> <>9__1;
					Func<CelestialBody, bool> predicate;
					if ((predicate = <>9__1) == null)
					{
						predicate = (<>9__1 = ((CelestialBody cb) => cb != home));
					}
					foreach (CelestialBody child2 in orbitingBodies.Where(predicate))
					{
						if (!child2.name.Equals(RuntimeUtility.mockBody.name))
						{
							yield return child2;
						}
					}
					IEnumerator<CelestialBody> enumerator2 = null;
				}
			}
			else if (id == "PlanetaryProgram")
			{
				foreach (CelestialBody body in FlightGlobals.Bodies[0].orbitingBodies)
				{
					if (body != home && body.Radius > 100.0 && body.pqsController != null && body.hasSolidSurface && !body.name.Equals(RuntimeUtility.mockBody.name))
					{
						yield return body;
					}
				}
				List<CelestialBody>.Enumerator enumerator = default(List<CelestialBody>.Enumerator);
			}
			else if (id == "GasGiantProgram")
			{
				foreach (CelestialBody body2 in FlightGlobals.Bodies[0].orbitingBodies)
				{
					if ((body2.pqsController == null || !body2.hasSolidSurface) && !body2.orbitingBodies.Contains(home) && body2.orbitingBodies.Count<CelestialBody>() >= 2 && body2.Radius > 100.0 && !body2.name.Equals(RuntimeUtility.mockBody.name))
					{
						yield return body2;
					}
				}
				List<CelestialBody>.Enumerator enumerator = default(List<CelestialBody>.Enumerator);
			}
			else if (id == "ImpactorProbes")
			{
				foreach (CelestialBody body3 in FlightGlobals.Bodies[0].orbitingBodies)
				{
					if (body3 != home)
					{
						if (body3.pqsController != null && body3.hasSolidSurface && !body3.name.Equals(RuntimeUtility.mockBody.name))
						{
							yield return body3;
						}
						foreach (CelestialBody childBody in body3.orbitingBodies)
						{
							if (childBody.pqsController != null && body3.hasSolidSurface && !childBody.name.Equals(RuntimeUtility.mockBody.name))
							{
								yield return childBody;
							}
						}
						List<CelestialBody>.Enumerator enumerator3 = default(List<CelestialBody>.Enumerator);
					}
					body3 = null;
				}
				List<CelestialBody>.Enumerator enumerator = default(List<CelestialBody>.Enumerator);
			}
			else if (id == "FlyByProbes")
			{
				foreach (CelestialBody body4 in FlightGlobals.Bodies[0].orbitingBodies)
				{
					if (body4 != home && !body4.orbitingBodies.Contains(home) && !body4.name.Equals(RuntimeUtility.mockBody.name))
					{
						yield return body4;
					}
				}
				List<CelestialBody>.Enumerator enumerator = default(List<CelestialBody>.Enumerator);
			}
			else
			{
				foreach (CelestialBody body5 in FlightGlobals.Bodies)
				{
					yield return body5;
				}
				List<CelestialBody>.Enumerator enumerator = default(List<CelestialBody>.Enumerator);
			}
			yield break;
			yield break;
		}

		public static string BodyList(IEnumerable<CelestialBody> bodies, string conjunction)
		{
			CelestialBody first = bodies.First<CelestialBody>();
			CelestialBody last = bodies.Last<CelestialBody>();
			string result = first.CleanDisplayName(false);
			Func<CelestialBody, bool> <>9__0;
			Func<CelestialBody, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((CelestialBody cb) => cb != first && cb != last));
			}
			foreach (CelestialBody body in bodies.Where(predicate))
			{
				result = result + ", " + body.CleanDisplayName(true);
			}
			if (last != first)
			{
				result = string.Concat(new string[]
				{
					result,
					" ",
					conjunction,
					" ",
					last.CleanDisplayName(true)
				});
			}
			return result;
		}
		private const double BARYCENTER_THRESHOLD = 100.0;
	}
}
