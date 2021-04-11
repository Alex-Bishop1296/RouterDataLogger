// Fires when page is loaded with OnInit
import { Component, OnInit } from '@angular/core';
// Call for our API
import { GetRoutersLogTableService }  from './services/get-routers-log-table.service';
import RouterLog from './models/RouterLog';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'Router Status Log';
  routerLogTable: RouterLog[] = [];
  routerLogFields = ['id', 'serial', 'timestamp', 'status'];

  constructor(private getTableService: GetRoutersLogTableService) {}
  //Start firing after component is mounted into the DOM
  ngOnInit(): void {
    this.getTableService.getTable()
    .subscribe(
      //assign local property to incoming api GET request
      table => {this.routerLogTable = table
                console.log(table)}
    )
  }
}
