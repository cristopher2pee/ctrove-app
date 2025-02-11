import { Injectable } from '@angular/core';
import { GOOGLE_MAP_URL, HttpService } from '../http.service';
import { environment } from 'src/environments/environment';
import { CommonService } from './common.service';
import { firstValueFrom } from 'rxjs';
import { DEFAULT_EMPTY_STRING, GEOLOCATION_NOT_SUPPORTED } from 'src/Utilities/common/app-strings';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  coordinates!: Coordinates
  currentLocation!: string

  constructor(private httpService: HttpService,
    private commonService: CommonService) { }

  getAddress = (lat: number = this.coordinates.latitude, long: number = this.coordinates.longitude) => this.httpService.get<any>(`${GOOGLE_MAP_URL}?latlng=${lat},${long}&key=${environment.googleMapApiKey}`)

  getCoordinates = () => this.coordinates

  getSavedAddress = async (includeLocal: boolean = true) => {
    if(this.currentLocation)
      return this.currentLocation
    
    if(!this.coordinates)
      return includeLocal ? this.getLocalTimezone() : null

    var response = await firstValueFrom(this.getAddress())
    if(response && response.results.length > 0)
      return this.currentLocation = response.results[0].formatted_address 
    else
      return DEFAULT_EMPTY_STRING
  }

  getCurrentLocation = () => {
    if(navigator.geolocation) 
      navigator.geolocation.getCurrentPosition((position: any) => {
        this.coordinates = position.coords
      })
    else 
      this.commonService.showErrorMessage(GEOLOCATION_NOT_SUPPORTED)
  }

  setPosition = (position: any) => this.coordinates = position.coords as Coordinates
  
  getLocalTimezone = () => Intl.DateTimeFormat().resolvedOptions().timeZone
}

export interface Coordinates {
  latitude: number,
  longitude: number
}
