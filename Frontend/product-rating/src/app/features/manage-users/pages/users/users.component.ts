import { ManageUsersService } from '../../services/manage-users-service';
import { UserManageHeaderData } from 'src/app/models/users/UserManageHeaderData';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ManageUserFilterData } from 'src/app/models/users/ManageUserFilterData';
import { RoleDisplay, Role } from 'src/app/models/Role';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  users: UserManageHeaderData[];
  filter: ManageUserFilterData = new ManageUserFilterData();
  pagination: PaginationParams = new PaginationParams();

  @ViewChild('lockoutTml') lockoutTml: TemplateRef<any>;
  @ViewChild('hdrTpl') hdrTpl: TemplateRef<any>;
  columns = [];

  roleDisplay = RoleDisplay;
  role = Role;

  constructor(private manageUsersService: ManageUsersService) { }

  ngOnInit() {
    //Todo: use server side paging.
    this.pagination.length = 1000;
    this.pagination.start = 1;

    this.columns = [
      { prop: 'email' },
      { prop: 'nickName' },
      { prop: 'role' },
      { prop: 'isLockedOut' },
      { name: 'id', cellTemplate: this.lockoutTml, headerTemplate: this.hdrTpl },
    ];
    this.listUsers();
  }

  reload() {
    this.listUsers();
  }

  listUsers() {
    this.manageUsersService.getUsers(this.filter, this.pagination)
      .subscribe(result => {
        this.users = result;
        console.log(this.users);
      })
  }

  changeIsLockedOutFilter(value) {
    this.filter.isLockedOut = value;
  }

  lockOut(userId) {
    this.manageUsersService.lockoutUser(userId)
      .subscribe(result => {
        this.reload();
      })
  }

  admit(userId) {
    this.manageUsersService.admitUser(userId)
      .subscribe(result => {
        this.reload();
      })
  }

}
