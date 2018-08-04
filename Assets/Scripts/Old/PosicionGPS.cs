using UnityEngine;
using System.Collections;
using System;

public class PosicionGPS  : MonoBehaviour{

	static readonly double R = 6371.0; // Radius of the earth in km
	static readonly double pi_sobre_180 = Math.PI / 180.0;
	public double lat,lon;

	public PosicionGPS(double x,double y){
		lat = x;
		lon = y;
	}

	public double calcularDistancia(PosicionGPS p){
		double dLat = (p.lat-this.lat) * pi_sobre_180;  // deg2rad below
		double dLon = (p.lon-this.lon) * pi_sobre_180; 

		double a = 
			Math.Sin(dLat/2) * Math.Sin(dLat/2) +
			Math.Cos(this.lat * pi_sobre_180) * Math.Cos(p.lat * pi_sobre_180) * 
			Math.Sin(dLon/2) * Math.Sin(dLon/2);

		double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a)); 
		double d = R * c; // Distance in km

		return d;
	}

	public static double calcularDistancia(PosicionGPS p1, PosicionGPS p2){
		double dLat = (p2.lat-p1.lat) * pi_sobre_180;  // deg2rad below
		double dLon = (p2.lon-p1.lon) * pi_sobre_180; 

		double a = 
			Math.Sin(dLat/2) * Math.Sin(dLat/2) +
			Math.Cos(p1.lat * pi_sobre_180) * Math.Cos(p2.lat * pi_sobre_180) * 
			Math.Sin(dLon/2) * Math.Sin(dLon/2);

		double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a)); 
		double d = R * c; // Distance in km

		return d;
	}
}
