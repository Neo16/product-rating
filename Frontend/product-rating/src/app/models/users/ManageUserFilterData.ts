import { Role } from '../Role';

export class ManageUserFilterData
{
    email: string;

    nickName: string;

    role: Role | null;

    isLockedOut: boolean | null;
}