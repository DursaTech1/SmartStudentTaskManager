<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProfile
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    Private components As System.ComponentModel.IContainer

    Friend WithEvents pnlTitleBar        As Panel
    Friend WithEvents btnClose           As Button
    Friend WithEvents btnMinimize        As Button
    Friend WithEvents lblFormTitle       As Label
    Friend WithEvents pnlCenter          As Panel
    Friend WithEvents pnlCard            As Panel
    Friend WithEvents pnlLeft            As Panel
    Friend WithEvents pnlAvatarRing      As Panel
    Friend WithEvents pnlAvatar          As Panel
    Friend WithEvents lblAvatarInitials  As Label
    Friend WithEvents btnUploadPicture   As Button
    Friend WithEvents lblDisplayName     As Label
    Friend WithEvents lblRoleBadge       As Label
    Friend WithEvents lblMemberSince     As Label
    Friend WithEvents pnlLeftDivider     As Panel
    Friend WithEvents pnlStatTotal       As Panel
    Friend WithEvents lblStatTasks       As Label
    Friend WithEvents lblStatTasksLbl    As Label
    Friend WithEvents pnlStatDone        As Panel
    Friend WithEvents lblStatDone        As Label
    Friend WithEvents lblStatDoneLbl     As Label
    Friend WithEvents pnlStatWeek        As Panel
    Friend WithEvents lblStatStreak      As Label
    Friend WithEvents lblStatStreakLbl   As Label
    Friend WithEvents lblChipEmail       As Label
    Friend WithEvents lblChipStudentID   As Label
    Friend WithEvents pnlRight           As Panel
    Friend WithEvents pnlRightContent    As Panel
    Friend WithEvents pnlSecInfo         As Panel
    Friend WithEvents lblSecInfoIcon     As Label
    Friend WithEvents lblSectionInfo     As Label
    Friend WithEvents pnlSecInfoLine     As Panel
    Friend WithEvents lblUsernameLbl     As Label
    Friend WithEvents txtUsername        As TextBox
    Friend WithEvents lblFullNameLbl     As Label
    Friend WithEvents txtFullName        As TextBox
    Friend WithEvents lblStudentIDLbl    As Label
    Friend WithEvents txtStudentID       As TextBox
    Friend WithEvents lblEmailLbl        As Label
    Friend WithEvents txtEmail           As TextBox
    Friend WithEvents pnlSecPwd          As Panel
    Friend WithEvents lblSecPwdIcon      As Label
    Friend WithEvents lblSectionPwd      As Label
    Friend WithEvents pnlSecPwdLine      As Panel
    Friend WithEvents lblCurrentPwdLbl   As Label
    Friend WithEvents txtCurrentPassword As TextBox
    Friend WithEvents lblNewPwdLbl       As Label
    Friend WithEvents txtNewPassword     As TextBox
    Friend WithEvents lblConfirmPwdLbl   As Label
    Friend WithEvents txtConfirmPassword As TextBox
    Friend WithEvents pnlButtons         As Panel
    Friend WithEvents btnSave            As Button
    Friend WithEvents btnCancel          As Button

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlTitleBar = New Panel()
        btnClose = New Button()
        btnMinimize = New Button()
        lblFormTitle = New Label()
        pnlCenter = New Panel()
        pnlCard = New Panel()
        pnlLeft = New Panel()
        pnlAvatarRing = New Panel()
        pnlAvatar = New Panel()
        btnUploadPicture = New Button()
        lblDisplayName = New Label()
        lblRoleBadge = New Label()
        lblMemberSince = New Label()
        pnlLeftDivider = New Panel()
        pnlStatTotal = New Panel()
        lblStatTasks = New Label()
        lblStatTasksLbl = New Label()
        pnlStatDone = New Panel()
        lblStatDone = New Label()
        lblStatDoneLbl = New Label()
        pnlStatWeek = New Panel()
        lblStatStreak = New Label()
        lblStatStreakLbl = New Label()
        lblChipEmail = New Label()
        lblChipStudentID = New Label()
        pnlRight = New Panel()
        pnlRightContent = New Panel()
        pnlSecInfo = New Panel()
        lblSecInfoIcon = New Label()
        lblSectionInfo = New Label()
        pnlSecInfoLine = New Panel()
        lblUsernameLbl = New Label()
        txtUsername = New TextBox()
        lblFullNameLbl = New Label()
        txtFullName = New TextBox()
        lblStudentIDLbl = New Label()
        txtStudentID = New TextBox()
        lblEmailLbl = New Label()
        txtEmail = New TextBox()
        pnlSecPwd = New Panel()
        lblSecPwdIcon = New Label()
        lblSectionPwd = New Label()
        pnlSecPwdLine = New Panel()
        lblCurrentPwdLbl = New Label()
        txtCurrentPassword = New TextBox()
        lblNewPwdLbl = New Label()
        txtNewPassword = New TextBox()
        lblConfirmPwdLbl = New Label()
        txtConfirmPassword = New TextBox()
        pnlButtons = New Panel()
        btnSave = New Button()
        btnCancel = New Button()
        lblAvatarInitials = New Label()
        pnlTitleBar.SuspendLayout()
        pnlCenter.SuspendLayout()
        pnlCard.SuspendLayout()
        pnlLeft.SuspendLayout()
        pnlAvatarRing.SuspendLayout()
        pnlStatTotal.SuspendLayout()
        pnlStatDone.SuspendLayout()
        pnlStatWeek.SuspendLayout()
        pnlRight.SuspendLayout()
        pnlRightContent.SuspendLayout()
        pnlSecInfo.SuspendLayout()
        pnlSecPwd.SuspendLayout()
        pnlButtons.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlTitleBar
        ' 
        pnlTitleBar.BackColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        pnlTitleBar.Controls.Add(btnClose)
        pnlTitleBar.Controls.Add(btnMinimize)
        pnlTitleBar.Controls.Add(lblFormTitle)
        pnlTitleBar.Dock = DockStyle.Top
        pnlTitleBar.Location = New Point(0, 0)
        pnlTitleBar.Name = "pnlTitleBar"
        pnlTitleBar.Size = New Size(1200, 54)
        pnlTitleBar.TabIndex = 1
        ' 
        ' btnClose
        ' 
        btnClose.Dock = DockStyle.Right
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(239), CByte(68), CByte(68))
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Location = New Point(1092, 0)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(54, 54)
        btnClose.TabIndex = 0
        btnClose.Text = "x"
        btnClose.UseVisualStyleBackColor = False
        ' 
        ' btnMinimize
        ' 
        btnMinimize.Dock = DockStyle.Right
        btnMinimize.FlatAppearance.BorderSize = 0
        btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(49), CByte(46), CByte(129))
        btnMinimize.FlatStyle = FlatStyle.Flat
        btnMinimize.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        btnMinimize.ForeColor = Color.White
        btnMinimize.Location = New Point(1146, 0)
        btnMinimize.Name = "btnMinimize"
        btnMinimize.Size = New Size(54, 54)
        btnMinimize.TabIndex = 1
        btnMinimize.Text = "-"
        btnMinimize.UseVisualStyleBackColor = False
        ' 
        ' lblFormTitle
        ' 
        lblFormTitle.AutoSize = True
        lblFormTitle.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        lblFormTitle.ForeColor = Color.White
        lblFormTitle.Location = New Point(20, 15)
        lblFormTitle.Name = "lblFormTitle"
        lblFormTitle.Size = New Size(125, 32)
        lblFormTitle.TabIndex = 2
        lblFormTitle.Text = "My Profile"
        ' 
        ' pnlCenter
        ' 
        pnlCenter.BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(251))
        pnlCenter.Controls.Add(pnlCard)
        pnlCenter.Dock = DockStyle.Fill
        pnlCenter.Location = New Point(0, 54)
        pnlCenter.Name = "pnlCenter"
        pnlCenter.Size = New Size(1200, 746)
        pnlCenter.TabIndex = 0
        ' 
        ' pnlCard
        ' 
        pnlCard.BackColor = Color.White
        pnlCard.Controls.Add(pnlLeft)
        pnlCard.Controls.Add(pnlRight)
        pnlCard.Dock = DockStyle.Fill
        pnlCard.Location = New Point(0, 0)
        pnlCard.Name = "pnlCard"
        pnlCard.Size = New Size(1200, 746)
        pnlCard.TabIndex = 0
        ' 
        ' pnlLeft
        ' 
        pnlLeft.BackColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        pnlLeft.Controls.Add(pnlAvatarRing)
        pnlLeft.Controls.Add(btnUploadPicture)
        pnlLeft.Controls.Add(lblDisplayName)
        pnlLeft.Controls.Add(lblRoleBadge)
        pnlLeft.Controls.Add(lblMemberSince)
        pnlLeft.Controls.Add(pnlLeftDivider)
        pnlLeft.Controls.Add(pnlStatTotal)
        pnlLeft.Controls.Add(pnlStatDone)
        pnlLeft.Controls.Add(pnlStatWeek)
        pnlLeft.Controls.Add(lblChipEmail)
        pnlLeft.Controls.Add(lblChipStudentID)
        pnlLeft.Dock = DockStyle.Left
        pnlLeft.Location = New Point(0, 0)
        pnlLeft.Name = "pnlLeft"
        pnlLeft.Size = New Size(320, 746)
        pnlLeft.TabIndex = 0
        ' 
        ' pnlAvatarRing
        ' 
        pnlAvatarRing.BackColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        pnlAvatarRing.Controls.Add(pnlAvatar)
        pnlAvatarRing.Location = New Point(95, 40)
        pnlAvatarRing.Name = "pnlAvatarRing"
        pnlAvatarRing.Size = New Size(130, 130)
        pnlAvatarRing.TabIndex = 0
        ' 
        ' pnlAvatar
        ' 
        pnlAvatar.BackColor = Color.Transparent
        pnlAvatar.Location = New Point(10, 10)
        pnlAvatar.Name = "pnlAvatar"
        pnlAvatar.Size = New Size(110, 110)
        pnlAvatar.TabIndex = 0
        pnlAvatar.Visible = False
        ' 
        ' btnUploadPicture
        ' 
        btnUploadPicture.BackColor = Color.FromArgb(CByte(49), CByte(46), CByte(129))
        btnUploadPicture.Cursor = Cursors.Hand
        btnUploadPicture.FlatAppearance.BorderSize = 0
        btnUploadPicture.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(67), CByte(56), CByte(202))
        btnUploadPicture.FlatStyle = FlatStyle.Flat
        btnUploadPicture.Font = New Font("Segoe UI", 9F)
        btnUploadPicture.ForeColor = Color.FromArgb(CByte(165), CByte(180), CByte(252))
        btnUploadPicture.Location = New Point(85, 178)
        btnUploadPicture.Name = "btnUploadPicture"
        btnUploadPicture.Size = New Size(150, 32)
        btnUploadPicture.TabIndex = 1
        btnUploadPicture.Text = "Change Photo"
        btnUploadPicture.UseVisualStyleBackColor = False
        ' 
        ' lblDisplayName
        ' 
        lblDisplayName.BackColor = Color.Transparent
        lblDisplayName.Font = New Font("Segoe UI Semibold", 14F, FontStyle.Bold)
        lblDisplayName.ForeColor = Color.White
        lblDisplayName.Location = New Point(0, 220)
        lblDisplayName.Name = "lblDisplayName"
        lblDisplayName.Size = New Size(320, 30)
        lblDisplayName.TabIndex = 2
        lblDisplayName.Text = "Username"
        lblDisplayName.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblRoleBadge
        ' 
        lblRoleBadge.BackColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        lblRoleBadge.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblRoleBadge.ForeColor = Color.White
        lblRoleBadge.Location = New Point(110, 258)
        lblRoleBadge.Name = "lblRoleBadge"
        lblRoleBadge.Size = New Size(100, 26)
        lblRoleBadge.TabIndex = 3
        lblRoleBadge.Text = "Student"
        lblRoleBadge.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblMemberSince
        ' 
        lblMemberSince.BackColor = Color.Transparent
        lblMemberSince.Font = New Font("Segoe UI", 9F, FontStyle.Italic)
        lblMemberSince.ForeColor = Color.FromArgb(CByte(165), CByte(180), CByte(252))
        lblMemberSince.Location = New Point(0, 292)
        lblMemberSince.Name = "lblMemberSince"
        lblMemberSince.Size = New Size(320, 22)
        lblMemberSince.TabIndex = 4
        lblMemberSince.Text = "Member since: -"
        lblMemberSince.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlLeftDivider
        ' 
        pnlLeftDivider.BackColor = Color.FromArgb(CByte(49), CByte(46), CByte(129))
        pnlLeftDivider.Location = New Point(20, 326)
        pnlLeftDivider.Name = "pnlLeftDivider"
        pnlLeftDivider.Size = New Size(280, 1)
        pnlLeftDivider.TabIndex = 5
        ' 
        ' pnlStatTotal
        ' 
        pnlStatTotal.BackColor = Color.White
        pnlStatTotal.Controls.Add(lblStatTasks)
        pnlStatTotal.Controls.Add(lblStatTasksLbl)
        pnlStatTotal.ForeColor = SystemColors.ActiveCaptionText
        pnlStatTotal.Location = New Point(20, 338)
        pnlStatTotal.Name = "pnlStatTotal"
        pnlStatTotal.Size = New Size(280, 56)
        pnlStatTotal.TabIndex = 6
        ' 
        ' lblStatTasks
        ' 
        lblStatTasks.BackColor = Color.Transparent
        lblStatTasks.Font = New Font("Segoe UI Semibold", 20F, FontStyle.Bold)
        lblStatTasks.ForeColor = Color.FromArgb(CByte(129), CByte(140), CByte(248))
        lblStatTasks.Location = New Point(200, 8)
        lblStatTasks.Name = "lblStatTasks"
        lblStatTasks.Size = New Size(64, 40)
        lblStatTasks.TabIndex = 0
        lblStatTasks.Text = "0"
        lblStatTasks.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblStatTasksLbl
        ' 
        lblStatTasksLbl.BackColor = Color.Transparent
        lblStatTasksLbl.Font = New Font("Segoe UI", 9F)
        lblStatTasksLbl.ForeColor = Color.Black
        lblStatTasksLbl.Location = New Point(16, 8)
        lblStatTasksLbl.Name = "lblStatTasksLbl"
        lblStatTasksLbl.Size = New Size(120, 40)
        lblStatTasksLbl.TabIndex = 1
        lblStatTasksLbl.Text = "Total Tasks"
        lblStatTasksLbl.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' pnlStatDone
        ' 
        pnlStatDone.BackColor = Color.White
        pnlStatDone.Controls.Add(lblStatDone)
        pnlStatDone.Controls.Add(lblStatDoneLbl)
        pnlStatDone.Location = New Point(20, 402)
        pnlStatDone.Name = "pnlStatDone"
        pnlStatDone.Size = New Size(280, 56)
        pnlStatDone.TabIndex = 7
        ' 
        ' lblStatDone
        ' 
        lblStatDone.BackColor = Color.Transparent
        lblStatDone.Font = New Font("Segoe UI Semibold", 20F, FontStyle.Bold)
        lblStatDone.ForeColor = Color.FromArgb(CByte(52), CByte(211), CByte(153))
        lblStatDone.Location = New Point(200, 8)
        lblStatDone.Name = "lblStatDone"
        lblStatDone.Size = New Size(64, 40)
        lblStatDone.TabIndex = 0
        lblStatDone.Text = "0"
        lblStatDone.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblStatDoneLbl
        ' 
        lblStatDoneLbl.BackColor = Color.Transparent
        lblStatDoneLbl.Font = New Font("Segoe UI", 9F)
        lblStatDoneLbl.ForeColor = Color.Black
        lblStatDoneLbl.Location = New Point(16, 8)
        lblStatDoneLbl.Name = "lblStatDoneLbl"
        lblStatDoneLbl.Size = New Size(120, 40)
        lblStatDoneLbl.TabIndex = 1
        lblStatDoneLbl.Text = "Completed"
        lblStatDoneLbl.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' pnlStatWeek
        ' 
        pnlStatWeek.BackColor = Color.White
        pnlStatWeek.Controls.Add(lblStatStreak)
        pnlStatWeek.Controls.Add(lblStatStreakLbl)
        pnlStatWeek.Location = New Point(20, 466)
        pnlStatWeek.Name = "pnlStatWeek"
        pnlStatWeek.Size = New Size(280, 56)
        pnlStatWeek.TabIndex = 8
        ' 
        ' lblStatStreak
        ' 
        lblStatStreak.BackColor = Color.Transparent
        lblStatStreak.Font = New Font("Segoe UI Semibold", 20F, FontStyle.Bold)
        lblStatStreak.ForeColor = Color.FromArgb(CByte(252), CByte(211), CByte(77))
        lblStatStreak.Location = New Point(200, 8)
        lblStatStreak.Name = "lblStatStreak"
        lblStatStreak.Size = New Size(64, 40)
        lblStatStreak.TabIndex = 0
        lblStatStreak.Text = "0"
        lblStatStreak.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblStatStreakLbl
        ' 
        lblStatStreakLbl.BackColor = Color.Transparent
        lblStatStreakLbl.Font = New Font("Segoe UI", 9F)
        lblStatStreakLbl.ForeColor = Color.Black
        lblStatStreakLbl.Location = New Point(16, 8)
        lblStatStreakLbl.Name = "lblStatStreakLbl"
        lblStatStreakLbl.Size = New Size(120, 40)
        lblStatStreakLbl.TabIndex = 1
        lblStatStreakLbl.Text = "This Week"
        lblStatStreakLbl.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblChipEmail
        ' 
        lblChipEmail.BackColor = Color.FromArgb(CByte(26), CByte(23), CByte(68))
        lblChipEmail.Font = New Font("Segoe UI", 9F)
        lblChipEmail.ForeColor = Color.FromArgb(CByte(165), CByte(180), CByte(252))
        lblChipEmail.Location = New Point(16, 540)
        lblChipEmail.Name = "lblChipEmail"
        lblChipEmail.Padding = New Padding(8, 0, 0, 0)
        lblChipEmail.Size = New Size(288, 28)
        lblChipEmail.TabIndex = 9
        lblChipEmail.Text = "  -"
        lblChipEmail.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblChipStudentID
        ' 
        lblChipStudentID.BackColor = Color.FromArgb(CByte(26), CByte(23), CByte(68))
        lblChipStudentID.Font = New Font("Segoe UI", 9F)
        lblChipStudentID.ForeColor = Color.FromArgb(CByte(165), CByte(180), CByte(252))
        lblChipStudentID.Location = New Point(16, 576)
        lblChipStudentID.Name = "lblChipStudentID"
        lblChipStudentID.Padding = New Padding(8, 0, 0, 0)
        lblChipStudentID.Size = New Size(288, 28)
        lblChipStudentID.TabIndex = 10
        lblChipStudentID.Text = "  -"
        lblChipStudentID.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' pnlRight
        ' 
        pnlRight.AutoScroll = True
        pnlRight.BackColor = Color.White
        pnlRight.Controls.Add(pnlRightContent)
        pnlRight.Dock = DockStyle.Fill
        pnlRight.Location = New Point(0, 0)
        pnlRight.Name = "pnlRight"
        pnlRight.Size = New Size(1200, 746)
        pnlRight.TabIndex = 1
        ' 
        ' pnlRightContent
        ' 
        pnlRightContent.BackColor = Color.White
        pnlRightContent.Controls.Add(pnlSecInfo)
        pnlRightContent.Controls.Add(lblUsernameLbl)
        pnlRightContent.Controls.Add(txtUsername)
        pnlRightContent.Controls.Add(lblFullNameLbl)
        pnlRightContent.Controls.Add(txtFullName)
        pnlRightContent.Controls.Add(lblStudentIDLbl)
        pnlRightContent.Controls.Add(txtStudentID)
        pnlRightContent.Controls.Add(lblEmailLbl)
        pnlRightContent.Controls.Add(txtEmail)
        pnlRightContent.Controls.Add(pnlSecPwd)
        pnlRightContent.Controls.Add(lblCurrentPwdLbl)
        pnlRightContent.Controls.Add(txtCurrentPassword)
        pnlRightContent.Controls.Add(lblNewPwdLbl)
        pnlRightContent.Controls.Add(txtNewPassword)
        pnlRightContent.Controls.Add(lblConfirmPwdLbl)
        pnlRightContent.Controls.Add(txtConfirmPassword)
        pnlRightContent.Controls.Add(pnlButtons)
        pnlRightContent.Location = New Point(60, 36)
        pnlRightContent.Name = "pnlRightContent"
        pnlRightContent.Size = New Size(700, 640)
        pnlRightContent.TabIndex = 0
        ' 
        ' pnlSecInfo
        ' 
        pnlSecInfo.BackColor = Color.Transparent
        pnlSecInfo.Controls.Add(lblSecInfoIcon)
        pnlSecInfo.Controls.Add(lblSectionInfo)
        pnlSecInfo.Controls.Add(pnlSecInfoLine)
        pnlSecInfo.Location = New Point(0, 0)
        pnlSecInfo.Name = "pnlSecInfo"
        pnlSecInfo.Size = New Size(700, 52)
        pnlSecInfo.TabIndex = 0
        ' 
        ' lblSecInfoIcon
        ' 
        lblSecInfoIcon.Location = New Point(0, 0)
        lblSecInfoIcon.Name = "lblSecInfoIcon"
        lblSecInfoIcon.Size = New Size(0, 0)
        lblSecInfoIcon.TabIndex = 0
        lblSecInfoIcon.Visible = False
        ' 
        ' lblSectionInfo
        ' 
        lblSectionInfo.BackColor = Color.Transparent
        lblSectionInfo.Font = New Font("Segoe UI Semibold", 15F, FontStyle.Bold)
        lblSectionInfo.ForeColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        lblSectionInfo.Location = New Point(0, 4)
        lblSectionInfo.Name = "lblSectionInfo"
        lblSectionInfo.Size = New Size(700, 36)
        lblSectionInfo.TabIndex = 1
        lblSectionInfo.Text = "Account Information"
        ' 
        ' pnlSecInfoLine
        ' 
        pnlSecInfoLine.BackColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        pnlSecInfoLine.Location = New Point(0, 48)
        pnlSecInfoLine.Name = "pnlSecInfoLine"
        pnlSecInfoLine.Size = New Size(700, 3)
        pnlSecInfoLine.TabIndex = 2
        ' 
        ' lblUsernameLbl
        ' 
        lblUsernameLbl.BackColor = Color.Transparent
        lblUsernameLbl.Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold)
        lblUsernameLbl.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblUsernameLbl.Location = New Point(0, 68)
        lblUsernameLbl.Name = "lblUsernameLbl"
        lblUsernameLbl.Size = New Size(680, 18)
        lblUsernameLbl.TabIndex = 1
        lblUsernameLbl.Text = "USERNAME"
        ' 
        ' txtUsername
        ' 
        txtUsername.BackColor = Color.FromArgb(CByte(249), CByte(250), CByte(251))
        txtUsername.BorderStyle = BorderStyle.FixedSingle
        txtUsername.Font = New Font("Segoe UI", 11F)
        txtUsername.Location = New Point(0, 88)
        txtUsername.Name = "txtUsername"
        txtUsername.ReadOnly = True
        txtUsername.Size = New Size(680, 37)
        txtUsername.TabIndex = 2
        ' 
        ' lblFullNameLbl
        ' 
        lblFullNameLbl.BackColor = Color.Transparent
        lblFullNameLbl.Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold)
        lblFullNameLbl.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblFullNameLbl.Location = New Point(0, 148)
        lblFullNameLbl.Name = "lblFullNameLbl"
        lblFullNameLbl.Size = New Size(330, 18)
        lblFullNameLbl.TabIndex = 3
        lblFullNameLbl.Text = "FULL NAME"
        ' 
        ' txtFullName
        ' 
        txtFullName.BorderStyle = BorderStyle.FixedSingle
        txtFullName.Font = New Font("Segoe UI", 11F)
        txtFullName.Location = New Point(0, 168)
        txtFullName.Name = "txtFullName"
        txtFullName.PlaceholderText = "e.g. Ahmad Murad"
        txtFullName.Size = New Size(330, 37)
        txtFullName.TabIndex = 4
        ' 
        ' lblStudentIDLbl
        ' 
        lblStudentIDLbl.BackColor = Color.Transparent
        lblStudentIDLbl.Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold)
        lblStudentIDLbl.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblStudentIDLbl.Location = New Point(350, 148)
        lblStudentIDLbl.Name = "lblStudentIDLbl"
        lblStudentIDLbl.Size = New Size(330, 18)
        lblStudentIDLbl.TabIndex = 5
        lblStudentIDLbl.Text = "STUDENT ID"
        ' 
        ' txtStudentID
        ' 
        txtStudentID.BorderStyle = BorderStyle.FixedSingle
        txtStudentID.Font = New Font("Segoe UI", 11F)
        txtStudentID.Location = New Point(350, 168)
        txtStudentID.Name = "txtStudentID"
        txtStudentID.PlaceholderText = "e.g. STU-2024-001"
        txtStudentID.Size = New Size(330, 37)
        txtStudentID.TabIndex = 6
        ' 
        ' lblEmailLbl
        ' 
        lblEmailLbl.BackColor = Color.Transparent
        lblEmailLbl.Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold)
        lblEmailLbl.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblEmailLbl.Location = New Point(0, 228)
        lblEmailLbl.Name = "lblEmailLbl"
        lblEmailLbl.Size = New Size(680, 18)
        lblEmailLbl.TabIndex = 7
        lblEmailLbl.Text = "EMAIL ADDRESS"
        ' 
        ' txtEmail
        ' 
        txtEmail.BorderStyle = BorderStyle.FixedSingle
        txtEmail.Font = New Font("Segoe UI", 11F)
        txtEmail.Location = New Point(0, 248)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(680, 37)
        txtEmail.TabIndex = 8
        ' 
        ' pnlSecPwd
        ' 
        pnlSecPwd.BackColor = Color.Transparent
        pnlSecPwd.Controls.Add(lblSecPwdIcon)
        pnlSecPwd.Controls.Add(lblSectionPwd)
        pnlSecPwd.Controls.Add(pnlSecPwdLine)
        pnlSecPwd.Location = New Point(0, 316)
        pnlSecPwd.Name = "pnlSecPwd"
        pnlSecPwd.Size = New Size(700, 52)
        pnlSecPwd.TabIndex = 9
        ' 
        ' lblSecPwdIcon
        ' 
        lblSecPwdIcon.Location = New Point(0, 0)
        lblSecPwdIcon.Name = "lblSecPwdIcon"
        lblSecPwdIcon.Size = New Size(0, 0)
        lblSecPwdIcon.TabIndex = 0
        lblSecPwdIcon.Visible = False
        ' 
        ' lblSectionPwd
        ' 
        lblSectionPwd.BackColor = Color.Transparent
        lblSectionPwd.Font = New Font("Segoe UI Semibold", 15F, FontStyle.Bold)
        lblSectionPwd.ForeColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        lblSectionPwd.Location = New Point(0, 4)
        lblSectionPwd.Name = "lblSectionPwd"
        lblSectionPwd.Size = New Size(700, 36)
        lblSectionPwd.TabIndex = 1
        lblSectionPwd.Text = "Change Password"
        ' 
        ' pnlSecPwdLine
        ' 
        pnlSecPwdLine.BackColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        pnlSecPwdLine.Location = New Point(0, 48)
        pnlSecPwdLine.Name = "pnlSecPwdLine"
        pnlSecPwdLine.Size = New Size(700, 3)
        pnlSecPwdLine.TabIndex = 2
        ' 
        ' lblCurrentPwdLbl
        ' 
        lblCurrentPwdLbl.BackColor = Color.Transparent
        lblCurrentPwdLbl.Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold)
        lblCurrentPwdLbl.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblCurrentPwdLbl.Location = New Point(0, 384)
        lblCurrentPwdLbl.Name = "lblCurrentPwdLbl"
        lblCurrentPwdLbl.Size = New Size(680, 18)
        lblCurrentPwdLbl.TabIndex = 10
        lblCurrentPwdLbl.Text = "CURRENT PASSWORD  (leave blank to keep unchanged)"
        ' 
        ' txtCurrentPassword
        ' 
        txtCurrentPassword.BorderStyle = BorderStyle.FixedSingle
        txtCurrentPassword.Font = New Font("Segoe UI", 11F)
        txtCurrentPassword.Location = New Point(0, 404)
        txtCurrentPassword.Name = "txtCurrentPassword"
        txtCurrentPassword.PasswordChar = "*"c
        txtCurrentPassword.Size = New Size(680, 37)
        txtCurrentPassword.TabIndex = 11
        ' 
        ' lblNewPwdLbl
        ' 
        lblNewPwdLbl.BackColor = Color.Transparent
        lblNewPwdLbl.Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold)
        lblNewPwdLbl.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblNewPwdLbl.Location = New Point(0, 462)
        lblNewPwdLbl.Name = "lblNewPwdLbl"
        lblNewPwdLbl.Size = New Size(330, 18)
        lblNewPwdLbl.TabIndex = 12
        lblNewPwdLbl.Text = "NEW PASSWORD"
        ' 
        ' txtNewPassword
        ' 
        txtNewPassword.BorderStyle = BorderStyle.FixedSingle
        txtNewPassword.Font = New Font("Segoe UI", 11F)
        txtNewPassword.Location = New Point(0, 482)
        txtNewPassword.Name = "txtNewPassword"
        txtNewPassword.PasswordChar = "*"c
        txtNewPassword.Size = New Size(330, 37)
        txtNewPassword.TabIndex = 13
        ' 
        ' lblConfirmPwdLbl
        ' 
        lblConfirmPwdLbl.BackColor = Color.Transparent
        lblConfirmPwdLbl.Font = New Font("Segoe UI Semibold", 8.5F, FontStyle.Bold)
        lblConfirmPwdLbl.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblConfirmPwdLbl.Location = New Point(350, 462)
        lblConfirmPwdLbl.Name = "lblConfirmPwdLbl"
        lblConfirmPwdLbl.Size = New Size(330, 18)
        lblConfirmPwdLbl.TabIndex = 14
        lblConfirmPwdLbl.Text = "CONFIRM PASSWORD"
        ' 
        ' txtConfirmPassword
        ' 
        txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle
        txtConfirmPassword.Font = New Font("Segoe UI", 11F)
        txtConfirmPassword.Location = New Point(350, 482)
        txtConfirmPassword.Name = "txtConfirmPassword"
        txtConfirmPassword.PasswordChar = "*"c
        txtConfirmPassword.Size = New Size(330, 37)
        txtConfirmPassword.TabIndex = 15
        ' 
        ' pnlButtons
        ' 
        pnlButtons.BackColor = Color.Transparent
        pnlButtons.Controls.Add(btnSave)
        pnlButtons.Controls.Add(btnCancel)
        pnlButtons.Location = New Point(0, 552)
        pnlButtons.Name = "pnlButtons"
        pnlButtons.Size = New Size(680, 56)
        pnlButtons.TabIndex = 16
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        btnSave.Cursor = Cursors.Hand
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(67), CByte(56), CByte(202))
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Segoe UI Semibold", 11F, FontStyle.Bold)
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(0, 0)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(330, 52)
        btnSave.TabIndex = 0
        btnSave.Text = "Save Changes"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.FromArgb(CByte(238), CByte(242), CByte(255))
        btnCancel.Cursor = Cursors.Hand
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(224), CByte(231), CByte(255))
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI Semibold", 11F, FontStyle.Bold)
        btnCancel.ForeColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        btnCancel.Location = New Point(350, 0)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(330, 52)
        btnCancel.TabIndex = 1
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
        ' 
        ' lblAvatarInitials
        ' 
        lblAvatarInitials.BackColor = Color.Transparent
        lblAvatarInitials.Dock = DockStyle.Fill
        lblAvatarInitials.Font = New Font("Segoe UI", 36F, FontStyle.Bold)
        lblAvatarInitials.ForeColor = Color.White
        lblAvatarInitials.Location = New Point(0, 0)
        lblAvatarInitials.Name = "lblAvatarInitials"
        lblAvatarInitials.Size = New Size(100, 23)
        lblAvatarInitials.TabIndex = 0
        lblAvatarInitials.Text = "?"
        lblAvatarInitials.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' frmProfile
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(251))
        ClientSize = New Size(1200, 800)
        Controls.Add(pnlCenter)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        MinimumSize = New Size(1200, 800)
        Name = "frmProfile"
        StartPosition = FormStartPosition.CenterScreen
        Text = "My Profile"
        WindowState = FormWindowState.Maximized
        pnlTitleBar.ResumeLayout(False)
        pnlTitleBar.PerformLayout()
        pnlCenter.ResumeLayout(False)
        pnlCard.ResumeLayout(False)
        pnlLeft.ResumeLayout(False)
        pnlAvatarRing.ResumeLayout(False)
        pnlStatTotal.ResumeLayout(False)
        pnlStatDone.ResumeLayout(False)
        pnlStatWeek.ResumeLayout(False)
        pnlRight.ResumeLayout(False)
        pnlRightContent.ResumeLayout(False)
        pnlRightContent.PerformLayout()
        pnlSecInfo.ResumeLayout(False)
        pnlSecPwd.ResumeLayout(False)
        pnlButtons.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
End Class
