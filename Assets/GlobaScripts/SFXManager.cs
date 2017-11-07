using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

/// <summary>
/// 
/// this scrips is under the Camera to stay close to the AudioListener
/// 
/// </summary>

public class SFXManager : MonoBehaviour {


	static readonly string SFXPath = "SFX/";

	// example		SFXtype.Coin , List<CoinSFX_1, CoinSFX_2, CoinSFX_3> 
	static Dictionary<SFXtype, List<AudioClip>> ClipsDictionary = new Dictionary<SFXtype, List<AudioClip>> ();

	// all the Audio Sources on this object
	static List<AudioSource> AllAudioSources = new List<AudioSource> ();

	// in case there was a lot of scrips calling SFX.PlaySFXFor at once 
	[SerializeField] int NumberOfSFXPlayedTogether = 5;

	#region debuging

//	[SerializeField] SFXtype t;
//	[SerializeField] List<AudioClip> AllClipsFor;
//	void Update(){
//		AllClipsFor = new List<AudioClip> (ClipsDictionary [t]);
//	}

	#endregion


	#region Setting clips 

	void Awake(){
		
		#region Fill the Clips dictionary

		SFXtype[] AllSFXTypes = System.Enum.GetValues (typeof(SFXtype)) as SFXtype[];

		foreach (SFXtype type in AllSFXTypes) {

			if(type != SFXtype.None){			// None is just for debuging

				ClipsDictionary.Add( type, new List<AudioClip> ( AllTypeClips(type)) );
					
			}

		}

		#endregion

		#region Setting AudioSources

		for (int x = 0; x < NumberOfSFXPlayedTogether; x++)
			gameObject.AddComponent<AudioSource> ();

		AllAudioSources = new List<AudioSource> ( GetComponents<AudioSource> () );

		#endregion

	}

	/// <summary>
	/// return a list of all the clips in the Resources -> SFXPath -> type folder name
	/// </summary>
	/// <param name="type">The sfx type.</param>
	List<AudioClip> AllTypeClips(SFXtype type){

		AudioClip[] Clips = Resources.LoadAll<AudioClip>( SFXPath + type.ToString() );


		List<AudioClip> typeClips = new List<AudioClip> ();

		foreach (AudioClip clip in Clips)
			typeClips.Add (clip);

		return typeClips;

	}


	#endregion


	#region PlayingSFX


	public static void PlaySFXFor(SFXtype type){			//print ("PlaySFXFor: " + type.ToString());

		List<AudioClip> TypeClips = new List<AudioClip> ( ClipsDictionary [type] );

		AudioClip clip = TypeClips [Random.Range (0, TypeClips.Count)];

		UnusedAudioSourcePlay (clip);

	}

	/// <summary>
	/// Only play the clip if there was unused Audio Source
	/// </summary>
	/// <param name="clip">The clip that needs to be played.</param>
	static void UnusedAudioSourcePlay(AudioClip clip){

		foreach (AudioSource AS in AllAudioSources) {

			if (!AS.isPlaying) {

				AS.clip = clip;
				AS.Play ();
				return;

			}

		}

	}

	#endregion


}