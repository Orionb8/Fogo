import { HttpClient } from "@angular/common/http";

export class BaseService<T> {
    constructor(protected http: HttpClient) {
    }

    fetch(request: Request | PageRequest) {
    }

    save(model: T) {
    }

    post(model: T) {
    }

    put(model: T) {
    }

    delete(model: T) {
    }
}

export class Request {
    constructor(args: {
        skip: 0,
        take: 25,
        filter?: Filter
    }) {
    }
}

export class PageRequest {
    constructor(args: {
        page: 1,
        size: 25,
        filter?: Filter
    }) {
    }
}

export class Filter {
    constructor(
        public path?: string,
        public value?: string,
        public operation = Operation.equal,
        public similarity = Similarity.any,
        public connector = Connector.and,
        public filters: Filter[] = []) {
    }
}

export enum Operation {
    equal,
    notEqual,
    lessThan,
    greaterThan,
    lessThanOrEqual,
    greaterThanOrEqual,
    contains,
    notContains,
    startsWith,
    notStartsWith,
    endsWith,
    notEndsWith,
    isNull,
    isNotNull,
    isEmpty,
    isNotEmpty
}

export enum Similarity {
    any,
    all
}

export enum Connector {
    and,
    or
}