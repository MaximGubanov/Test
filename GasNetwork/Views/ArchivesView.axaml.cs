using Avalonia.Controls;

namespace GasNetwork.Views;

public partial class ArchivesView : UserControl
{
    public ArchivesView()
    {
        InitializeComponent();
        //TODO: ��������� �������� ��� ����������� ������
        RangeTemplates.Items = new[]
        {
            "���� ���", "�������", "��� ������", "���� �����", "���� ���",
            "������� ���", "�����", "������� ������", "������� �����", "������� ���",
            "������ ��� � \"�����������\""
        };
    }
}