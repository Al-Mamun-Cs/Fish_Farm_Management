export interface User {
    id: number;
    roleName: string;
    userName: string;
    firstName: string;
    lastName: string;
    password: string;
    confirmPassword: string;
    // isAnonymous: boolean; 
    // lastActivityDate: Date;
    supplierId:number,
    departmentPostPositionId:number,
    branchId: string;
    firstLevel: string;
    secondLevel?: string;
    thirdLevel?: string;
    fourthLevel?: string;
    // winPassword: boolean;
    // userFullName: string;
    // approvedUser: boolean;
    phoneNumber : string;
    email : string;
    traineeId : string;
    // securityQustion : string;
    // answer : string;
    // isFirstTime: boolean;
    // isLogin: number;
    // ipAddress: string;
    // hostName: string;
    photoPath:string;
    signaturePath:string;
    isActive: boolean;   
}