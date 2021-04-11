import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import RouterLog from '../models/RouterLog';

@Injectable({
  providedIn: 'root'
})
export class GetRoutersLogTableService {
  apiUrl = "http://localhost:43353/api/routerLogs" //https://localhost:44364/api/routerLogs is our api address on our local machine
  constructor(private http: HttpClient) { }

  getTable() {
    return this.http.get<RouterLog[]>(this.apiUrl);
  }
}

