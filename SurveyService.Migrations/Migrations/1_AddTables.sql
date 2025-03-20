create table survey
(
    id          bigint primary key generated always as identity not null,
    description text                                            not null,
    questionids bigint[] not null
);

create table interview
(
    id       bigint primary key generated always as identity not null,
    userid   bigint                                          not null,
    surveyid bigint references survey (id)                   not null
);

create table question
(
    id          bigint primary key generated always as identity not null,
    description text                                            not null,
    surveyid    bigint references survey (id)                   not null
);

create table answer
(
    id          bigint primary key generated always as identity not null,
    description text                                            not null,
    questionid  bigint references question (id)                 not null
);
create index idx_answer_questionid on answer (questionid);

create table result
(
    id          bigint primary key generated always as identity not null,
    interviewid bigint references interview (id)                not null,
    answerid    bigint references answer (id)                   not null
);