
export class CreateItem {
    constructor(public description: string) {
    }
}

export class UpdateItem extends CreateItem {
    constructor(public id: number = 0, public description: string = '', public isCompleted: boolean = false) {
        super(description);
    }
}